using LuaInputs.Code;
using LuaInputs.LuaLang;
using REghZyFramework.Utilities;

namespace LuaInputs {
    public class MainViewModel : BaseViewModel {
        public CodeEditorViewModel CodeEditor { get; }
        public LuaMachine Machine { get; }

        public MainViewModel() {
            this.CodeEditor = new CodeEditorViewModel();
            this.Machine = new LuaMachine(this.CodeEditor);
        }
    }
}