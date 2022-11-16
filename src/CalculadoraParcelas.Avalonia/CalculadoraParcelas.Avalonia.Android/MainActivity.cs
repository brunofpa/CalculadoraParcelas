using Android.App;
using Android.Content.PM;
using Avalonia;
using Avalonia.Android;

namespace CalculadoraParcelas.Android
{
    [Activity(Label = "CalculadoraParcelas.Android", Theme = "@style/MyTheme.NoActionBar", Icon = "@drawable/icon", ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize)]
    public class MainActivity : AvaloniaMainActivity
    {
    }
}
