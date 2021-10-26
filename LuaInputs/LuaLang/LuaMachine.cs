using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LuaInputs.Code;
using NLua;

namespace LuaInputs.LuaLang {
    public class LuaMachine {
        public CodeEditorViewModel CodeEditor { get; }

        public Lua Machine { get; }

        public LuaMachine(CodeEditorViewModel codeEditor) {
            this.CodeEditor = codeEditor;
            this.Machine = new Lua();
            this.Machine["api"] = this;
        }

        public void Print(string value = "") {
            this.CodeEditor.WriteConsole(value == null ? "" : value);
        }

        public void PrintLine(string value = "") {
            this.CodeEditor.WriteConsoleLine(value == null ? "" : value);
        }
    }
}
