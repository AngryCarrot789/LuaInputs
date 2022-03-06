using System;
using System.Runtime.InteropServices;
using LuaInputs.Native.Keys;

namespace LuaInputs.Native.Output {
    [StructLayout(LayoutKind.Sequential)]
    public struct KEYBDINPUT {
        public VirtualKey wVk;
        public ScanCode wScan;
        public KEYEVENTF dwFlags;
        public int time;
        public UIntPtr dwExtraInfo;
    }
}