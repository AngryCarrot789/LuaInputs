using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Input;
using LuaInputs.OLD__NATIVE;

namespace LuaInputs.Native {
    public class RawInput {
        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);
        private const int WH_KEYBOARD_LL = 13;
        private const int HC_ACTION = 0;

        /// <summary>
        /// Raw key event callback
        /// </summary>
        private readonly Predicate<KeyEvent> callback;

        /// <summary>
        /// The system hook id
        /// </summary>
        private IntPtr hookId = IntPtr.Zero;

        /// <summary>
        /// States whether the system keyboard event hook is setup.
        /// </summary>
        public bool IsHookSetup { get; private set; }

        public RawInput(Predicate<KeyEvent> callback) {
            this.callback = callback;
        }

        /// <summary>
        /// Hooks/Sets up this application for receiving keydown callbacks
        /// </summary>
        public void Hook() {
            hookId = SetHook(HookCallback);
            IsHookSetup = true;
        }

        /// <summary>
        /// Unhooks this application, stopping it from receiving keydown callbacks
        /// </summary>
        public void Unhook() {
            UnhookWindowsHookEx(hookId);
            IsHookSetup = false;
        }

        /// <summary>
        /// Sets up the Key Up/Down event hooks.
        /// </summary>
        /// <param name="proc">The callback method to be called when a key up/down occours</param>
        /// <returns></returns>
        private static IntPtr SetHook(LowLevelKeyboardProc proc) {
            using (Process curProcess = Process.GetCurrentProcess()) {
                using (ProcessModule curModule = curProcess.MainModule) {
                    if (curModule == null) {
                        throw new Exception("Current process module was null");
                    }

                    return SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
                }
            }
        }

        /// <summary>
        /// Processes a raw key event
        /// </summary>
        /// <param name="vk">The virtual key that this event is about</param>
        /// <param name="repeat">The number of repeats this key has</param>
        /// <param name="scan">The scan code, aka, hardware key code</param>
        /// <param name="isExtended">States if the key event originated from an addition key on an enhanced keyboard</param>
        /// <param name="isAltDown">Whether the ALT key was pressed during this key event</param>
        /// <param name="previousState">States if the given key was already down during the event (aka if it's a repeat character)</param>
        /// <param name="transisionState">States whether the event came from pressing a key, or releasing a key. Aka, up/true, down/false</param>
        private bool ProcessKeyRaw(short vk, short repeat, byte scan, bool isExtended, bool isAltDown, bool previousState, bool transisionState) {
            return this.callback(new KeyEvent(vk, repeat, scan, isExtended, isAltDown, previousState, transisionState));
        }

        /// <summary>
        /// The method called when a key up/down occours across the system.
        /// </summary>
        /// <param name="nCode">Hook message indicator. If less than 0, <see cref="CallNextHookEx"/> MUST be called and the value of this function must be retured by <see cref="CallNextHookEx"/></param>
        /// <param name="wParamPtr">Keyboard message (WM_KEYDOWN, MW_KEYUP, WM_SYSKEYDOWN or WM_SYSKEYUP)</param>
        /// <param name="lParamPtr">Contains the bits of the repeat count, scan code, extended-flag key, context code, previous key-state flag, and transition-state flag</param>
        /// <returns></returns>
        private IntPtr HookCallback(int nCode, IntPtr wParamPtr, IntPtr lParamPtr) {
            if (nCode < 0) {
                return CallNextHookEx(hookId, nCode, wParamPtr, lParamPtr);
            }

            if (nCode == HC_ACTION) {
                uint wParam = (uint) wParamPtr;
                uint lParam = (uint) lParamPtr;
                short vk     = (short) (wParam & 0b00000000000000001111111111111111);
                short repeat = (short) (lParam & 0b00000000000000001111111111111111);
                byte scanCode = (byte) (lParam & 0b00000000111111110000000000000000);
                bool extended =        (lParam & 0b00000001000000000000000000000000) == 1;
                byte reserved = (byte) (lParam & 0b00011110000000000000000000000000);
                bool context =         (lParam & 0b00100000000000000000000000000000) == 1;
                bool prevKeyState =    (lParam & 0b01000000000000000000000000000000) == 1;
                bool transitionState = (lParam & 0b10000000000000000000000000000000) == 1;
                if (ProcessKeyRaw(vk, repeat, scanCode, extended, context, prevKeyState, transitionState)) {
                    return (IntPtr) 1;
                }
            }

            return CallNextHookEx(hookId, nCode, wParamPtr, lParamPtr);
        }

        #region Native Methods

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        #endregion
    }
}