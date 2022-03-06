using System;
using System.Windows;
using System.Windows.Input;
using LuaInputs.Code;
using LuaInputs.LuaLang;
using LuaInputs.Native;
using LuaInputs.Native.Input;
using REghZy.MVVM.Commands;
using REghZy.MVVM.ViewModels;

namespace LuaInputs {
    public class MainViewModel : BaseViewModel {
        public CodeEditorViewModel CodeEditor { get; }
        public LuaMachine Machine { get; }

        public RawInput Input { get; }

        public ICommand RegisterLuaCommand { get; }
        public ICommand UnregisterLuaCommand { get; }

        public MainViewModel() {
            this.Input = new RawInput(OnKeyEvent);
            this.Input.Hook();
            this.CodeEditor = new CodeEditorViewModel();
            this.Machine = new LuaMachine(this.CodeEditor);
            ServiceLocator.Console = this.CodeEditor;

            this.RegisterLuaCommand = new RelayCommand(RegisterLua);
            this.UnregisterLuaCommand = new RelayCommand(UnregisterLua);
        }

        public void RegisterLua() {
            if (this.Machine.IsRunning) {
                UnregisterLua();
            }

            try {
                this.Machine.Run();
            }
            catch (Exception e) {
                MessageBox.Show(e.Message);
            }
        }

        public void UnregisterLua() {
            if (this.Machine.IsRunning) {
                this.Machine.Stop();
            }
        }

        private bool OnKeyEvent(KeyEvent e) {
            if (!this.Machine.IsRunning) {
                return false;
            }

            foreach (Predicate<KeyEvent> callback in this.Machine.Callbacks) {
                try {
                    if (callback(e)) {
                        return true;
                    }
                }
                catch (Exception ex) {
                    UnregisterLua();
                    ServiceLocator.Console.PrintLine(ex.ToString());
                    ServiceLocator.Console.PrintLine("Stopping LUA machine for safety");
                    return true;
                }

            }

            return false;
        }
    }
}