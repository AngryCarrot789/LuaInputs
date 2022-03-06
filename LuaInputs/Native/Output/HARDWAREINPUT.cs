using System.Runtime.InteropServices;

namespace LuaInputs.Native.Output {
    [StructLayout(LayoutKind.Sequential)]
    public struct HARDWAREINPUT {
        public int uMsg;
        public short wParamL;
        public short wParamH;
    }
}