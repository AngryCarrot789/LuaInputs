using System;
using System.Collections.Generic;
using System.Reflection;

namespace LuaInputs.Native.Keys {
    public static class KeyUtils {
        private static readonly Dictionary<string, VirtualKey> NAME_TO_KEY = new Dictionary<string, VirtualKey>();
        private static readonly Dictionary<string, ScanCode> NAME_TO_SCANCODE = new Dictionary<string, ScanCode>();

        static KeyUtils() {
            foreach (FieldInfo info in typeof(VirtualKey).GetFields()) {
                if (info.IsLiteral) {
                    VirtualKey key = (VirtualKey) info.GetValue(null);
                    NAME_TO_KEY[key.ToString()] = key;
                }
            }

            foreach (FieldInfo info in typeof(ScanCode).GetFields()) {
                if (info.IsLiteral) {
                    ScanCode scanCode = (ScanCode) info.GetValue(null);
                    NAME_TO_SCANCODE[scanCode.ToString()] = scanCode;
                }
            }
        }

        public static VirtualKey GetKey(this ScanCode code) {
            switch (code) {
                case ScanCode.LBUTTON: return VirtualKey.LBUTTON;
                case ScanCode.CANCEL: return VirtualKey.CANCEL;
                case ScanCode.BACK: return VirtualKey.BACK;
                case ScanCode.TAB: return VirtualKey.TAB;
                case ScanCode.CLEAR: return VirtualKey.CLEAR;
                case ScanCode.RETURN: return VirtualKey.RETURN;
                case ScanCode.SHIFT: return VirtualKey.SHIFT;
                case ScanCode.CONTROL: return VirtualKey.CONTROL;
                case ScanCode.MENU: return VirtualKey.MENU;
                case ScanCode.CAPITAL: return VirtualKey.CAPITAL;
                case ScanCode.ESCAPE: return VirtualKey.ESCAPE;
                case ScanCode.SPACE: return VirtualKey.SPACE;
                case ScanCode.PRIOR: return VirtualKey.PRIOR;
                case ScanCode.NEXT: return VirtualKey.NEXT;
                case ScanCode.END: return VirtualKey.END;
                case ScanCode.HOME: return VirtualKey.HOME;
                case ScanCode.LEFT: return VirtualKey.LEFT;
                case ScanCode.UP: return VirtualKey.UP;
                case ScanCode.RIGHT: return VirtualKey.RIGHT;
                case ScanCode.DOWN: return VirtualKey.DOWN;
                case ScanCode.SNAPSHOT: return VirtualKey.SNAPSHOT;
                case ScanCode.INSERT: return VirtualKey.INSERT;
                case ScanCode.DELETE: return VirtualKey.DELETE;
                case ScanCode.HELP: return VirtualKey.HELP;
                case ScanCode.KEY_0: return VirtualKey.KEY_0;
                case ScanCode.KEY_1: return VirtualKey.KEY_1;
                case ScanCode.KEY_2: return VirtualKey.KEY_2;
                case ScanCode.KEY_3: return VirtualKey.KEY_3;
                case ScanCode.KEY_4: return VirtualKey.KEY_4;
                case ScanCode.KEY_5: return VirtualKey.KEY_5;
                case ScanCode.KEY_6: return VirtualKey.KEY_6;
                case ScanCode.KEY_7: return VirtualKey.KEY_7;
                case ScanCode.KEY_8: return VirtualKey.KEY_8;
                case ScanCode.KEY_9: return VirtualKey.KEY_9;
                case ScanCode.KEY_A: return VirtualKey.KEY_A;
                case ScanCode.KEY_B: return VirtualKey.KEY_B;
                case ScanCode.KEY_C: return VirtualKey.KEY_C;
                case ScanCode.KEY_D: return VirtualKey.KEY_D;
                case ScanCode.KEY_E: return VirtualKey.KEY_E;
                case ScanCode.KEY_F: return VirtualKey.KEY_F;
                case ScanCode.KEY_G: return VirtualKey.KEY_G;
                case ScanCode.KEY_H: return VirtualKey.KEY_H;
                case ScanCode.KEY_I: return VirtualKey.KEY_I;
                case ScanCode.KEY_J: return VirtualKey.KEY_J;
                case ScanCode.KEY_K: return VirtualKey.KEY_K;
                case ScanCode.KEY_L: return VirtualKey.KEY_L;
                case ScanCode.KEY_M: return VirtualKey.KEY_M;
                case ScanCode.KEY_N: return VirtualKey.KEY_N;
                case ScanCode.KEY_O: return VirtualKey.KEY_O;
                case ScanCode.KEY_P: return VirtualKey.KEY_P;
                case ScanCode.KEY_Q: return VirtualKey.KEY_Q;
                case ScanCode.KEY_R: return VirtualKey.KEY_R;
                case ScanCode.KEY_S: return VirtualKey.KEY_S;
                case ScanCode.KEY_T: return VirtualKey.KEY_T;
                case ScanCode.KEY_U: return VirtualKey.KEY_U;
                case ScanCode.KEY_V: return VirtualKey.KEY_V;
                case ScanCode.KEY_W: return VirtualKey.KEY_W;
                case ScanCode.KEY_X: return VirtualKey.KEY_X;
                case ScanCode.KEY_Y: return VirtualKey.KEY_Y;
                case ScanCode.KEY_Z: return VirtualKey.KEY_Z;
                case ScanCode.LWIN: return VirtualKey.LWIN;
                case ScanCode.RWIN: return VirtualKey.RWIN;
                case ScanCode.APPS: return VirtualKey.APPS;
                case ScanCode.SLEEP: return VirtualKey.SLEEP;
                case ScanCode.MULTIPLY: return VirtualKey.MULTIPLY;
                case ScanCode.ADD: return VirtualKey.ADD;
                case ScanCode.SUBTRACT: return VirtualKey.SUBTRACT;
                case ScanCode.DIVIDE: return VirtualKey.DIVIDE;
                case ScanCode.F1: return VirtualKey.F1;
                case ScanCode.F2: return VirtualKey.F2;
                case ScanCode.F3: return VirtualKey.F3;
                case ScanCode.F4: return VirtualKey.F4;
                case ScanCode.F5: return VirtualKey.F5;
                case ScanCode.F6: return VirtualKey.F6;
                case ScanCode.F7: return VirtualKey.F7;
                case ScanCode.F8: return VirtualKey.F8;
                case ScanCode.F9: return VirtualKey.F9;
                case ScanCode.F10: return VirtualKey.F10;
                case ScanCode.F11: return VirtualKey.F11;
                case ScanCode.F12: return VirtualKey.F12;
                case ScanCode.F13: return VirtualKey.F13;
                case ScanCode.F14: return VirtualKey.F14;
                case ScanCode.F15: return VirtualKey.F15;
                case ScanCode.F16: return VirtualKey.F16;
                case ScanCode.F17: return VirtualKey.F17;
                case ScanCode.F18: return VirtualKey.F18;
                case ScanCode.F19: return VirtualKey.F19;
                case ScanCode.F20: return VirtualKey.F20;
                case ScanCode.F21: return VirtualKey.F21;
                case ScanCode.F22: return VirtualKey.F22;
                case ScanCode.F23: return VirtualKey.F23;
                case ScanCode.F24: return VirtualKey.F24;
                case ScanCode.NUMLOCK: return VirtualKey.NUMLOCK;
                case ScanCode.RSHIFT: return VirtualKey.RSHIFT;
                case ScanCode.OEM_1: return VirtualKey.OEM_1;
                case ScanCode.OEM_PLUS: return VirtualKey.OEM_PLUS;
                case ScanCode.OEM_COMMA: return VirtualKey.OEM_COMMA;
                case ScanCode.OEM_MINUS: return VirtualKey.OEM_MINUS;
                case ScanCode.OEM_PERIOD: return VirtualKey.OEM_PERIOD;
                case ScanCode.OEM_3: return VirtualKey.OEM_3;
                case ScanCode.OEM_4: return VirtualKey.OEM_4;
                case ScanCode.OEM_5: return VirtualKey.OEM_5;
                case ScanCode.OEM_6: return VirtualKey.OEM_6;
                case ScanCode.OEM_7: return VirtualKey.OEM_7;
                case ScanCode.OEM_102: return VirtualKey.OEM_102;
                case ScanCode.ZOOM: return VirtualKey.ZOOM;
                default: throw new ArgumentOutOfRangeException(nameof(code), code, null);
            }
        }

        public static ScanCode GetScanCode(this VirtualKey code) {
            switch (code) {
                case VirtualKey.LBUTTON: return ScanCode.LBUTTON;
                case VirtualKey.CANCEL: return ScanCode.CANCEL;
                case VirtualKey.BACK: return ScanCode.BACK;
                case VirtualKey.TAB: return ScanCode.TAB;
                case VirtualKey.CLEAR: return ScanCode.CLEAR;
                case VirtualKey.RETURN: return ScanCode.RETURN;
                case VirtualKey.SHIFT: return ScanCode.SHIFT;
                case VirtualKey.CONTROL: return ScanCode.CONTROL;
                case VirtualKey.MENU: return ScanCode.MENU;
                case VirtualKey.CAPITAL: return ScanCode.CAPITAL;
                case VirtualKey.ESCAPE: return ScanCode.ESCAPE;
                case VirtualKey.SPACE: return ScanCode.SPACE;
                case VirtualKey.PRIOR: return ScanCode.PRIOR;
                case VirtualKey.NEXT: return ScanCode.NEXT;
                case VirtualKey.END: return ScanCode.END;
                case VirtualKey.HOME: return ScanCode.HOME;
                case VirtualKey.LEFT: return ScanCode.LEFT;
                case VirtualKey.UP: return ScanCode.UP;
                case VirtualKey.RIGHT: return ScanCode.RIGHT;
                case VirtualKey.DOWN: return ScanCode.DOWN;
                case VirtualKey.SNAPSHOT: return ScanCode.SNAPSHOT;
                case VirtualKey.INSERT: return ScanCode.INSERT;
                case VirtualKey.DELETE: return ScanCode.DELETE;
                case VirtualKey.HELP: return ScanCode.HELP;
                case VirtualKey.KEY_0: return ScanCode.KEY_0;
                case VirtualKey.KEY_1: return ScanCode.KEY_1;
                case VirtualKey.KEY_2: return ScanCode.KEY_2;
                case VirtualKey.KEY_3: return ScanCode.KEY_3;
                case VirtualKey.KEY_4: return ScanCode.KEY_4;
                case VirtualKey.KEY_5: return ScanCode.KEY_5;
                case VirtualKey.KEY_6: return ScanCode.KEY_6;
                case VirtualKey.KEY_7: return ScanCode.KEY_7;
                case VirtualKey.KEY_8: return ScanCode.KEY_8;
                case VirtualKey.KEY_9: return ScanCode.KEY_9;
                case VirtualKey.KEY_A: return ScanCode.KEY_A;
                case VirtualKey.KEY_B: return ScanCode.KEY_B;
                case VirtualKey.KEY_C: return ScanCode.KEY_C;
                case VirtualKey.KEY_D: return ScanCode.KEY_D;
                case VirtualKey.KEY_E: return ScanCode.KEY_E;
                case VirtualKey.KEY_F: return ScanCode.KEY_F;
                case VirtualKey.KEY_G: return ScanCode.KEY_G;
                case VirtualKey.KEY_H: return ScanCode.KEY_H;
                case VirtualKey.KEY_I: return ScanCode.KEY_I;
                case VirtualKey.KEY_J: return ScanCode.KEY_J;
                case VirtualKey.KEY_K: return ScanCode.KEY_K;
                case VirtualKey.KEY_L: return ScanCode.KEY_L;
                case VirtualKey.KEY_M: return ScanCode.KEY_M;
                case VirtualKey.KEY_N: return ScanCode.KEY_N;
                case VirtualKey.KEY_O: return ScanCode.KEY_O;
                case VirtualKey.KEY_P: return ScanCode.KEY_P;
                case VirtualKey.KEY_Q: return ScanCode.KEY_Q;
                case VirtualKey.KEY_R: return ScanCode.KEY_R;
                case VirtualKey.KEY_S: return ScanCode.KEY_S;
                case VirtualKey.KEY_T: return ScanCode.KEY_T;
                case VirtualKey.KEY_U: return ScanCode.KEY_U;
                case VirtualKey.KEY_V: return ScanCode.KEY_V;
                case VirtualKey.KEY_W: return ScanCode.KEY_W;
                case VirtualKey.KEY_X: return ScanCode.KEY_X;
                case VirtualKey.KEY_Y: return ScanCode.KEY_Y;
                case VirtualKey.KEY_Z: return ScanCode.KEY_Z;
                case VirtualKey.LWIN: return ScanCode.LWIN;
                case VirtualKey.RWIN: return ScanCode.RWIN;
                case VirtualKey.APPS: return ScanCode.APPS;
                case VirtualKey.SLEEP: return ScanCode.SLEEP;
                case VirtualKey.MULTIPLY: return ScanCode.MULTIPLY;
                case VirtualKey.ADD: return ScanCode.ADD;
                case VirtualKey.SUBTRACT: return ScanCode.SUBTRACT;
                case VirtualKey.DIVIDE: return ScanCode.DIVIDE;
                case VirtualKey.F1: return ScanCode.F1;
                case VirtualKey.F2: return ScanCode.F2;
                case VirtualKey.F3: return ScanCode.F3;
                case VirtualKey.F4: return ScanCode.F4;
                case VirtualKey.F5: return ScanCode.F5;
                case VirtualKey.F6: return ScanCode.F6;
                case VirtualKey.F7: return ScanCode.F7;
                case VirtualKey.F8: return ScanCode.F8;
                case VirtualKey.F9: return ScanCode.F9;
                case VirtualKey.F10: return ScanCode.F10;
                case VirtualKey.F11: return ScanCode.F11;
                case VirtualKey.F12: return ScanCode.F12;
                case VirtualKey.F13: return ScanCode.F13;
                case VirtualKey.F14: return ScanCode.F14;
                case VirtualKey.F15: return ScanCode.F15;
                case VirtualKey.F16: return ScanCode.F16;
                case VirtualKey.F17: return ScanCode.F17;
                case VirtualKey.F18: return ScanCode.F18;
                case VirtualKey.F19: return ScanCode.F19;
                case VirtualKey.F20: return ScanCode.F20;
                case VirtualKey.F21: return ScanCode.F21;
                case VirtualKey.F22: return ScanCode.F22;
                case VirtualKey.F23: return ScanCode.F23;
                case VirtualKey.F24: return ScanCode.F24;
                case VirtualKey.NUMLOCK: return ScanCode.NUMLOCK;
                case VirtualKey.RSHIFT: return ScanCode.RSHIFT;
                case VirtualKey.OEM_1: return ScanCode.OEM_1;
                case VirtualKey.OEM_PLUS: return ScanCode.OEM_PLUS;
                case VirtualKey.OEM_COMMA: return ScanCode.OEM_COMMA;
                case VirtualKey.OEM_MINUS: return ScanCode.OEM_MINUS;
                case VirtualKey.OEM_PERIOD: return ScanCode.OEM_PERIOD;
                case VirtualKey.OEM_3: return ScanCode.OEM_3;
                case VirtualKey.OEM_4: return ScanCode.OEM_4;
                case VirtualKey.OEM_5: return ScanCode.OEM_5;
                case VirtualKey.OEM_6: return ScanCode.OEM_6;
                case VirtualKey.OEM_7: return ScanCode.OEM_7;
                case VirtualKey.OEM_102: return ScanCode.OEM_102;
                case VirtualKey.ZOOM: return ScanCode.ZOOM;
                default: throw new ArgumentOutOfRangeException(nameof(code), code, null);
            }
        }

        public static VirtualKey GetKey(string name) {
            if (NAME_TO_KEY.TryGetValue(name, out VirtualKey key)) {
                return key;
            }

            throw new Exception("Unknown key: " + name);
        }

        public static ScanCode GetScanCode(string name) {
            if (NAME_TO_SCANCODE.TryGetValue(name, out ScanCode scanCode)) {
                return scanCode;
            }

            throw new Exception("Unknown scancode: " + name);
        }
    }
}