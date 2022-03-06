using System.Runtime.InteropServices;
using LuaInputs.Native.Keys;

namespace LuaInputs.Native.Output {
    public static class RawOutput {
        private static readonly INPUT[] SINGLE = new INPUT[1];

        /// <summary>
        /// Synthesizes keystrokes, mouse motions, and button clicks.
        /// </summary>
        [DllImport("user32.dll")]
        private static extern uint SendInput(uint nInputs, [MarshalAs(UnmanagedType.LPArray), In] INPUT[] pInputs, int cbSize);

        public static void SendKeyInput(VirtualKey key, ScanCode scanCode, bool isDown, KEYEVENTF flags) {
            if (isDown) {
                SendKeyDown(key, scanCode, flags);
            }
            else {
                SendKeyUp(key, scanCode, flags);
            }
        }

        public static void SendKeyDown(VirtualKey key, ScanCode scanCode, KEYEVENTF flags) {
            SINGLE[0] = new INPUT() {
                type = INPUT_TYPE.INPUT_KEYBOARD,
                ki = new KEYBDINPUT() {
                    wScan = scanCode,
                    wVk = key,
                    dwFlags = flags & ~KEYEVENTF.KEYUP
                }
            };

            SendInput(1u, SINGLE, INPUT.SIZE);
        }

        public static void SendKeyUp(VirtualKey key, ScanCode scanCode, KEYEVENTF flags) {
            SINGLE[0] = new INPUT() {
                type = INPUT_TYPE.INPUT_KEYBOARD,
                ki = new KEYBDINPUT() {
                    wScan = scanCode,
                    wVk = key,
                    dwFlags = flags | KEYEVENTF.KEYUP
                }
            };

            SendInput(1u, SINGLE, INPUT.SIZE);
        }

        public static void SendKeyInputs(INPUT[] inputs) {
            SendInput((uint) inputs.Length, inputs, INPUT.SIZE);
        }
    }
}