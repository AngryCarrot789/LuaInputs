using LuaInputs.Native.Keys;

namespace LuaInputs.LuaLang {
    /// <summary>
    /// An event for a single key
    /// </summary>
    public class KeyEvent {
        private readonly bool isKeyUp;

        public VirtualKey Key => (VirtualKey)this.VirtualKey;
        public uint VirtualKey { get; }

        public uint ScanCode { get; }

        public bool IsExtended { get; }

        public bool IsAltDown { get; }

        public bool IsKeyDown => !this.isKeyUp;

        public KeyEvent(uint virtualKey, uint scanCode, bool isExtended, bool isAltDown, uint keyFlag) {
            this.VirtualKey = virtualKey;
            this.ScanCode = scanCode;
            this.IsExtended = isExtended;
            this.IsAltDown = isAltDown;
            this.isKeyUp = keyFlag != 0x100; // WM_KEYUP = 0x101, x0100 = WM_KEYDOWN
        }
    }
}