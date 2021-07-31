using Avalonia;
using Avalonia.Controls;
using Avalonia.ReactiveUI;
using AVCalculator.Controller;
using AVCalculator.View;

namespace AVCalculator
{
    internal class Program
    {
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        public static void Main(string[] args)
        {
            BuildAvaloniaApp().Start(AppMain, args);
        }

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
        {
            return AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .LogToTrace()
                .UseReactiveUI();
        }

        // Your application's entry point. Here you can initialize your MVVM framework, DI
        // container, etc.
        private static void AppMain(Application app, string[] args)
        {
            var mainWindowController = new MainWindowController();

            var window = new MainWindow
            {
                MwController = mainWindowController,
                DataContext = mainWindowController
            };

            app.Run(window);
        }
    }
}