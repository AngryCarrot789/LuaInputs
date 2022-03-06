using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using REghZy.MVVM.Commands;
using REghZy.MVVM.ViewModels;

namespace LuaInputs.Code {
    public class CodeEditorViewModel : BaseViewModel, IConsole {
        private string filePath;

        private string codeText;
        private string consoleText;

        public string CodeText {
            get => this.codeText;
            set => RaisePropertyChanged(ref this.codeText, value);
        }

        public string ConsoleText {
            get => this.consoleText;
            set => RaisePropertyChanged(ref this.consoleText, value);
        }

        public string FilePath {
            get => this.filePath;
            set => RaisePropertyChanged(ref this.filePath, value);
        }

        public ICommand OpenFileCommand { get; }
        public ICommand SaveFileCommand { get; }
        public ICommand SaveFileAsCommand { get; }
        public ICommand ClearConsoleCommand { get; }
        public ICommand ClearCodeCommand { get; }

        public CodeEditorViewModel() {
            this.CodeText = "";
            this.CodeText += "function onKey(key, isDown, keyName, scanCode, isExtended)\n";
            this.CodeText += "    local stat = \"up\"\n";
            this.CodeText += "    if isDown then\n";
            this.CodeText += "        stat = \"down\"\n";
            this.CodeText += "    end\n";
            this.CodeText += "    local ex = \"no\"\n";
            this.CodeText += "    if isExtended then\n";
            this.CodeText += "        ex = \"yes\"\n";
            this.CodeText += "    end\n";
            this.CodeText += "\n";
            this.CodeText += "    rz:PrintLine(keyName .. \" -> \" .. stat .. \" -> \" .. ex)\n";
            this.CodeText += "    return true\n";
            this.CodeText += "end\n";
            this.CodeText += "\n";
            this.CodeText += "rz:RegisterCallback(onKey)\n";
            this.ConsoleText = "";
            this.OpenFileCommand = new RelayCommand(Open);
            this.SaveFileCommand = new RelayCommand(Save);
            this.SaveFileAsCommand = new RelayCommand(SaveAs);
            this.ClearCodeCommand = new RelayCommand(() => this.CodeText = "");
            this.ClearConsoleCommand = new RelayCommand(() => this.ConsoleText = "");
        }

        public void WriteConsole(string message) {
            this.ConsoleText += message;
        }

        public void WriteConsoleLine(string message) {
            this.ConsoleText += (message + '\n');
            ServiceLocator.ConsoleView.ScrollToBottom();
        }

        public void Open() {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == true) {
                if (!File.Exists(dialog.FileName)) {
                    MessageBox.Show($"[ERR001] '{dialog.FileName}' does not exist!");
                    return;
                }

                if (new FileInfo(dialog.FileName).Length > 20971520) {
                    MessageBox.Show($"[ERR003] '{dialog.FileName}' was bigger than 20mb!");
                    return;
                }

                this.FilePath = dialog.FileName;
                this.CodeText = File.ReadAllText(dialog.FileName);
            }
        }

        public void Save() {
            if (File.Exists(this.FilePath)) {
                try {
                    File.WriteAllText(this.FilePath, this.CodeText);
                }
                catch (Exception e) {
                    MessageBox.Show($"[ERR002] Failed to save file: {e.Message}");
                }
            }
            else {
                SaveAs();
            }
        }

        public void SaveAs() {
            SaveFileDialog dialog = new SaveFileDialog();
            if (dialog.ShowDialog() == true) {
                try {
                    File.WriteAllText(dialog.FileName, this.CodeText);
                    this.FilePath = dialog.FileName;
                }
                catch (Exception e) {
                    MessageBox.Show($"[ERR004] Failed to save file: {e.Message}");
                }
            }
        }

        public void Print(string text) {
            WriteConsole(text);
        }

        public void PrintLine(string text) {
            WriteConsoleLine(text);
        }

        public void Clear() {
            this.ConsoleText = "";
        }
    }
}