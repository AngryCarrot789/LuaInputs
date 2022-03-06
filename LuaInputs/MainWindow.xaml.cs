using System.Windows;
using LuaInputs.Code;

namespace LuaInputs {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IConsoleView {
        public MainWindow() {
            InitializeComponent();
            this.DataContext = new MainViewModel();
            ServiceLocator.ConsoleView = this;
        }

        public void ScrollToBottom() {
            this.consoleBox.ScrollToEnd();
        }
    }
}
