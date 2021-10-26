using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using LuaInputs.Native;
using Microsoft.Win32;
using REghZyFramework.Utilities;

namespace LuaInputs.Code {
    public class CodeEditorViewModel : BaseViewModel {
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
            this.ConsoleText = "";
            this.OpenFileCommand = new Command(() => Open());
            this.SaveFileCommand = new Command(() => Save());
            this.SaveFileAsCommand = new Command(() => SaveAs());
            this.ClearCodeCommand = new Command(() => this.CodeText = "");
            this.ClearConsoleCommand = new Command(() => this.ConsoleText = "");
        }

        public void WriteConsole(string message) {
            this.ConsoleText += message;
        }

        public void WriteConsoleLine(string message) {
            this.ConsoleText += (message + '\n');
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
    }
}