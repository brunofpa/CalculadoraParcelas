using ModernWpf;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;

namespace CalculadoraParcelasModernWpf
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        [Flags]
        public enum DwmWindowAttribute : uint
        {
            DWMWA_USE_IMMERSIVE_DARK_MODE = 20,
            DWMWA_SYSTEMBACKDROP_TYPE = 38
        }

        [Flags]
        public enum DwmSystemBackdropType : uint
        {
            DWMSBT_AUTO,
            DWMSBT_NONE,
            DWMSBT_MAINWINDOW,
            DWMSBT_TRANSIENTWINDOW,
            DWMSBT_TABBEDWINDOW,
        }

        [LibraryImport("dwmapi.dll")]
        private static partial int DwmSetWindowAttribute(IntPtr hwnd, DwmWindowAttribute dwAttribute, ref int pvAttribute, int cbAttribute);

        public static int SetWindowAttribute(IntPtr hwnd, DwmWindowAttribute attribute, int parameter)
            => DwmSetWindowAttribute(hwnd, attribute, ref parameter, sizeof(int));

        [SupportedOSPlatform("windows10.0.18362.0")]
        private void MainWindow_ContentRendered(object sender, EventArgs e)
        {
            var hWnd = (HwndSource)sender;
            UpdateStyleAttributes(hWnd);

            ThemeManager.Current.ActualApplicationThemeChanged += (s, e) =>
            {
                UpdateStyleAttributes(hWnd);
            };
        }

        [SupportedOSPlatform("windows10.0.18362.0")]
        private static void UpdateStyleAttributes(HwndSource hWnd)
        {
            var isDarkTheme = ThemeManager.Current.ActualApplicationTheme == ApplicationTheme.Dark;

            _ = SetWindowAttribute(hWnd.Handle, DwmWindowAttribute.DWMWA_USE_IMMERSIVE_DARK_MODE, isDarkTheme ? 1 : 0);
            _ = SetWindowAttribute(hWnd.Handle, DwmWindowAttribute.DWMWA_SYSTEMBACKDROP_TYPE, (int)DwmSystemBackdropType.DWMSBT_MAINWINDOW);
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
