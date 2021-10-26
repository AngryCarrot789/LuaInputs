using System.Windows.Forms;
using System.Windows.Input;

namespace LuaInputs.Native {
    /// <summary>
    /// An event for a single key
    /// </summary>
    public class KeyEvent {
        private readonly short virtualKey;
        private readonly short repeats;
        private readonly byte scanCode;
        private readonly bool isExtended;
        private readonly bool isAltDown;
        private readonly bool previousState;
        private readonly bool isKeyUp;

        public VirtualKey Key => (VirtualKey) this.virtualKey;
        public short VirtualKey => this.virtualKey;
        public short Repeat => this.repeats;
        public byte ScanCode => this.scanCode;
        public bool IsExtended => this.isExtended;
        public bool IsAltDown => this.isAltDown;
        public bool PreviousState => this.previousState;
        public bool IsKeyUp => this.isKeyUp;
        public bool IsKeyDown => !this.isKeyUp;

        public KeyEvent(short virtualKey, short repeats, byte scanCode, bool isExtended, bool isAltDown, bool previousState, bool isKeyUp) {
            this.virtualKey = virtualKey;
            this.repeats = repeats;
            this.scanCode = scanCode;
            this.isExtended = isExtended;
            this.isAltDown = isAltDown;
            this.previousState = previousState;
            this.isKeyUp = isKeyUp;
        }
    }
}