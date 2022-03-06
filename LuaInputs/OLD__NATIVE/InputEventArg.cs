using System;

namespace LuaInputs.OLD__NATIVE {
    public class InputEventArg : EventArgs {
        public InputEventArg(KeyPressEvent arg) {
            this.KeyPressEvent = arg;
        }

        private InputEventArg() { }

        public KeyPressEvent KeyPressEvent { get; private set; }
    }
}
