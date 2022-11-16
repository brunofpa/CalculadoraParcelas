using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using CalculadoraParcelas.Views;
using FluentAvalonia.Styling;

namespace CalculadoraParcelas
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }
        
        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow();
                var thm = AvaloniaLocator.Current.GetService<FluentAvaloniaTheme>();
                thm?.ForceWin32WindowToTheme(desktop.MainWindow, "Dark");
            }
            else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
            {
                singleViewPlatform.MainView = new MainView();
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}