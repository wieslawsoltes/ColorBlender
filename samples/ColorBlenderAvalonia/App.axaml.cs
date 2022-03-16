using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using ColorBlender;

namespace ColorBlenderAvalonia
{
    public class App : Application
    {
        public static ColorMatch Design { get; } = new ColorMatch(213, 46, 49);

        [STAThread]
        public static void Main(string[] args) => BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);

        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .LogToTrace();

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);

            Name = "ColorBlender .NET";
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow()
                {
                    DataContext = new ColorMatch(213, 46, 49)
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
