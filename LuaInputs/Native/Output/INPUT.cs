using System.Runtime.InteropServices;

namespace LuaInputs.Native.Output {
    [StructLayout(LayoutKind.Explicit)]
    public struct INPUT {
        [FieldOffset(0)]
        public INPUT_TYPE type;

        [FieldOffset(4)]
        public MOUSEINPUT mi;
        [FieldOffset(4)]
        public KEYBDINPUT ki;
        [FieldOffset(4)]
        public HARDWAREINPUT hi;

        public static int SIZE => Marshal.SizeOf(typeof(INPUT));
    }
}