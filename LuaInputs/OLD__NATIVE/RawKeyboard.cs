using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;

namespace LuaInputs.OLD__NATIVE {
    public sealed class RawKeyboard {
        private readonly Dictionary<IntPtr, KeyPressEvent> _deviceList = new Dictionary<IntPtr, KeyPressEvent>();
        public delegate void DeviceEventHandler(object sender, InputEventArg e);
        public event DeviceEventHandler KeyPressed;
        readonly object _padLock = new object();
        public int NumberOfKeyboards { get; private set; }
        static InputData _rawBuffer;

        public bool CaptureOnlyIfTopMostWindow { get; set; }

        public RawKeyboard(IntPtr hwnd) {
            RawInputDevice[] rid = new RawInputDevice[1];

            rid[0].UsagePage = HidUsagePage.GENERIC;
            rid[0].Usage = HidUsage.Keyboard;
            rid[0].Flags = RawInputDeviceFlags.INPUTSINK;
            rid[0].Target = hwnd;

            if (!Win32.RegisterRawInputDevices(rid, (uint)rid.Length, (uint)Marshal.SizeOf(rid[0]))) {
                throw new ApplicationException("Failed to register raw input device(s).");
            }
        }

        public void EnumerateDevices() {
            lock (this._padLock) {
                this._deviceList.Clear();

                int keyboardNumber = 0;

                KeyPressEvent globalDevice = new KeyPressEvent {
                    DeviceName = "Global Keyboard",
                    DeviceHandle = IntPtr.Zero,
                    DeviceType = Win32.GetDeviceType(DeviceType.RimTypekeyboard),
                    Name = "Fake Keyboard. Some keys (ZOOM, MUTE, VOLUMEUP, VOLUMEDOWN) are sent to rawinput with a handle of zero.",
                    Source = keyboardNumber++.ToString(CultureInfo.InvariantCulture)
                };

                this._deviceList.Add(globalDevice.DeviceHandle, globalDevice);

                int numberOfDevices = 0;
                uint deviceCount = 0;
                int dwSize = (Marshal.SizeOf(typeof(Rawinputdevicelist)));

                if (Win32.GetRawInputDeviceList(IntPtr.Zero, ref deviceCount, (uint)dwSize) == 0) {
                    IntPtr pRawInputDeviceList = Marshal.AllocHGlobal((int)(dwSize * deviceCount));
                    Win32.GetRawInputDeviceList(pRawInputDeviceList, ref deviceCount, (uint)dwSize);

                    for (int i = 0; i < deviceCount; i++) {
                        uint pcbSize = 0;

                        // On Window 8 64bit when compiling against .Net > 3.5 using .ToInt32 you will generate an arithmetic overflow. Leave as it is for 32bit/64bit applications
                        Rawinputdevicelist rid = (Rawinputdevicelist)Marshal.PtrToStructure(new IntPtr((pRawInputDeviceList.ToInt64() + (dwSize * i))), typeof(Rawinputdevicelist));

                        Win32.GetRawInputDeviceInfo(rid.hDevice, RawInputDeviceInfo.RIDI_DEVICENAME, IntPtr.Zero, ref pcbSize);

                        if (pcbSize <= 0)
                            continue;

                        IntPtr pData = Marshal.AllocHGlobal((int)pcbSize);
                        Win32.GetRawInputDeviceInfo(rid.hDevice, RawInputDeviceInfo.RIDI_DEVICENAME, pData, ref pcbSize);
                        string deviceName = Marshal.PtrToStringAnsi(pData);

                        if (rid.dwType == DeviceType.RimTypekeyboard || rid.dwType == DeviceType.RimTypeHid) {
                            string deviceDesc = Win32.GetDeviceDescription(deviceName);

                            KeyPressEvent dInfo = new KeyPressEvent {
                                DeviceName = Marshal.PtrToStringAnsi(pData),
                                DeviceHandle = rid.hDevice,
                                DeviceType = Win32.GetDeviceType(rid.dwType),
                                Name = deviceDesc,
                                Source = keyboardNumber++.ToString(CultureInfo.InvariantCulture)
                            };

                            if (!this._deviceList.ContainsKey(rid.hDevice)) {
                                numberOfDevices++;
                                this._deviceList.Add(rid.hDevice, dInfo);
                            }
                        }

                        Marshal.FreeHGlobal(pData);
                    }

                    Marshal.FreeHGlobal(pRawInputDeviceList);

                    this.NumberOfKeyboards = numberOfDevices;
                    Debug.WriteLine("EnumerateDevices() found {0} Keyboard(s)", this.NumberOfKeyboards);
                    return;
                }
            }

            throw new Win32Exception(Marshal.GetLastWin32Error());
        }

        public bool ProcessRawInput(IntPtr hdevice) {
            if (this._deviceList.Count == 0)
                return false;
            if (this.CaptureOnlyIfTopMostWindow && !Win32.InputInForeground(_rawBuffer.header.wParam))
                return false;

            int dwSize = 0;
            Win32.GetRawInputData(hdevice, DataCommand.RID_INPUT, IntPtr.Zero, ref dwSize, Marshal.SizeOf(typeof(Rawinputheader)));

            if (dwSize != Win32.GetRawInputData(hdevice, DataCommand.RID_INPUT, out _rawBuffer, ref dwSize, Marshal.SizeOf(typeof(Rawinputheader)))) {
                Debug.WriteLine("Error getting the rawinput buffer");
                return false;
            }

            int virtualKey = _rawBuffer.data.keyboard.VKey;
            int makeCode = _rawBuffer.data.keyboard.Makecode;
            int flags = _rawBuffer.data.keyboard.Flags;

            if (virtualKey == Win32.KEYBOARD_OVERRUN_MAKE_CODE)
                return false;

            bool isE0BitSet = ((flags & Win32.RI_KEY_E0) != 0);

            KeyPressEvent keyPressEvent;

            if (this._deviceList.ContainsKey(_rawBuffer.header.hDevice)) {
                lock (this._padLock) {
                    keyPressEvent = this._deviceList[_rawBuffer.header.hDevice];
                }
            }
            else {
                Debug.WriteLine("Handle: {0} was not in the device list.", _rawBuffer.header.hDevice);
                return false;
            }

            bool isBreakBitSet = ((flags & Win32.RI_KEY_BREAK) != 0);

            keyPressEvent.KeyPressState = isBreakBitSet ? "BREAK" : "MAKE";
            keyPressEvent.Message = _rawBuffer.data.keyboard.Message;
            keyPressEvent.VKeyName = KeyMapper.GetKeyName(VirtualKeyCorrection(virtualKey, isE0BitSet, makeCode)).ToUpper();
            keyPressEvent.VKey = virtualKey;

            if (KeyPressed != null) {
                KeyPressed(this, new InputEventArg(keyPressEvent));
            }

            return true;
        }

        private static int VirtualKeyCorrection(int virtualKey, bool isE0BitSet, int makeCode) {
            int correctedVKey = virtualKey;
            if (_rawBuffer.header.hDevice == IntPtr.Zero) {
                // When hDevice is 0 and the vkey is VK_CONTROL indicates the ZOOM key
                if (_rawBuffer.data.keyboard.VKey == Win32.VK_CONTROL) {
                    correctedVKey = Win32.VK_ZOOM;
                }
            }
            else {
                switch (virtualKey) {
                    // Right-hand CTRL and ALT have their e0 bit set 
                    case Win32.VK_CONTROL:
                        correctedVKey = isE0BitSet ? Win32.VK_RCONTROL : Win32.VK_LCONTROL;
                        break;
                    case Win32.VK_MENU:
                        correctedVKey = isE0BitSet ? Win32.VK_RMENU : Win32.VK_LMENU;
                        break;
                    case Win32.VK_SHIFT:
                        correctedVKey = makeCode == Win32.SC_SHIFT_R ? Win32.VK_RSHIFT : Win32.VK_LSHIFT;
                        break;
                    default:
                        correctedVKey = virtualKey;
                        break;
                }
            }

            return correctedVKey;
        }
    }
}