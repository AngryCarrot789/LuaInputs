using LuaInputs.Code;
using REghZy.MVVM.IoC;

namespace LuaInputs {
    public static class ServiceLocator {
        private static readonly SimpleIoC IoC = new SimpleIoC();

        public static IConsole Console {
            get => IoC.GetService<IConsole>();
            set => IoC.SetService(value);
        }

        public static IConsoleView ConsoleView {
            get => IoC.GetService<IConsoleView>();
            set => IoC.SetService(value);
        }
    }
}