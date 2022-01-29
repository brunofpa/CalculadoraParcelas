using ModernWpf;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;

namespace CalculadoraParcelasModernWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [Flags]
        public enum DwmWindowAttribute : uint
        {
            DWMWA_USE_IMMERSIVE_DARK_MODE = 20,
            DWMWA_MICA_EFFECT = 1029
        }

        [DllImport("dwmapi.dll")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, DwmWindowAttribute dwAttribute, ref int pvAttribute, int cbAttribute);

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_ContentRendered(object sender, EventArgs e)
        {
            if (!OperatingSystem.IsWindowsVersionAtLeast(10, 0, 18362, 0))
                return;

            var hWnd = (HwndSource)sender;
            // Apply Mica brush
            UpdateStyleAttributes(hWnd);


            ThemeManager.Current.ActualApplicationThemeChanged += (s, e) =>
            {
                if (!OperatingSystem.IsWindowsVersionAtLeast(10, 0, 18362, 0))
                    return;

                UpdateStyleAttributes(hWnd);
            };
            
        }

        [SupportedOSPlatform("windows10.0.18362.0")]
        private static void UpdateStyleAttributes(HwndSource hWnd)
        {
            var isDarkTheme = ThemeManager.Current.ActualApplicationTheme == ApplicationTheme.Dark;

            int trueValue = 0x01;
            int falseValue = 0x00;

            if (isDarkTheme)
                _ = DwmSetWindowAttribute(hWnd.Handle, DwmWindowAttribute.DWMWA_USE_IMMERSIVE_DARK_MODE, ref trueValue, Marshal.SizeOf(typeof(int)));
            else
                _ = DwmSetWindowAttribute(hWnd.Handle, DwmWindowAttribute.DWMWA_USE_IMMERSIVE_DARK_MODE, ref falseValue, Marshal.SizeOf(typeof(int)));

            _ = DwmSetWindowAttribute(hWnd.Handle, DwmWindowAttribute.DWMWA_MICA_EFFECT, ref trueValue, Marshal.SizeOf(typeof(int)));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (!OperatingSystem.IsWindowsVersionAtLeast(10, 0, 18362, 0))
                return;

            var presentationSource = PresentationSource.FromVisual(sender as Visual);

            presentationSource.ContentRendered += MainWindow_ContentRendered;
        }
    }
}
