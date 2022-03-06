using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using LuaInputs.Code;
using LuaInputs.Native;
using LuaInputs.Native.Keys;
using LuaInputs.Native.Output;
using NLua;

namespace LuaInputs.LuaLang {
    public class LuaMachine {
        [LuaHideAttribute]
        public List<Predicate<KeyEvent>> Callbacks { get; }

        [LuaHideAttribute]
        public CodeEditorViewModel CodeEditor { get; }

        [LuaHideAttribute]
        public Lua Machine { get; private set; }

        public bool IsRunning { get; private set; }

        public LuaMachine(CodeEditorViewModel codeEditor) {
            this.CodeEditor = codeEditor;
            this.Callbacks = new List<Predicate<KeyEvent>>(8);
        }

        public void Run() {
            if (this.IsRunning) {
                throw new Exception("Already running");
            }

            this.IsRunning = true;
            this.Machine = new Lua();
            this.Machine["rz"] = this;
            this.Machine.DoString(this.CodeEditor.CodeText);
        }

        public void Stop() {
            if (!this.IsRunning) {
                throw new Exception("Not running!");
            }

            this.Machine.Dispose();
            this.Machine = null;
            this.Callbacks.Clear();
            this.IsRunning = false;
        }

        public void Sleep(int time) {
            Thread.Sleep(time);
        }

        public void Print(string value = "") {
                this.CodeEditor.WriteConsole(value ?? "");
                ServiceLocator.ConsoleView.ScrollToBottom();
        }

        public void PrintLine(string value = "") {
            ServiceLocator.Console.PrintLine("[Lua] " + value);
            ServiceLocator.ConsoleView.ScrollToBottom();
        }

        public void SendKey(string key, bool down, bool isExtended = false) {
            VirtualKey vkey = KeyUtils.GetKey(key);
            RawOutput.SendKeyInput(vkey, vkey.GetScanCode(), down, isExtended ? KEYEVENTF.EXTENDEDKEY : 0u);
        }

        public void SendKey(int key, bool down, bool isExtended = false) {
            VirtualKey vkey = (VirtualKey) key;
            RawOutput.SendKeyInput(vkey, vkey.GetScanCode(), down, isExtended ? KEYEVENTF.EXTENDEDKEY : 0u);
        }

        public void RegisterCallback(LuaFunction function) {
            if (function == null) {
                ServiceLocator.Console.PrintLine("[C#] [Warning] Attempted to register null function");
                return;
            }

            this.Callbacks.Add((e) => {
                object[] ret = function.Call((int) e.Key, e.IsKeyDown, e.Key.ToString(), e.ScanCode, e.IsExtended);
                if (ret == null || ret.Length == 0) {
                    ServiceLocator.Console.PrintLine("[C#] [Warning] Registered callback did not contain a return value. Defaulting to 'false'");
                }
                else if (ret[0] is bool b) {
                    return b;
                }
                else {
                    ServiceLocator.Console.PrintLine("[C#] [Warning] Registered callback's first return value was not a boolean. Defaulting to 'false'");
                }

                return false;
            });
        }
    }
}
