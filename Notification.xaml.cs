using MinimalisticWPF.Theme;
using MinimalisticWPF.TransitionSystem;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MinimalisticWPF.Controls
{
    public enum NotificationTypes
    {
        Ask,
        Message,
        Warn
    }

    [DataContextConfig(nameof(Notification), "MinimalisticWPF.Controls.ViewModel")]
    public partial class Notification : Window
    {
        private static TaskCompletionSource<bool>? _tcs;
        private static Notification Instance { get; set; } = new();

        internal Notification()
        {
            InitializeComponent();
            Show();
        }

        public static async Task<bool> Ask(string info, bool isTopmost = true)
        {
            return await OpenWindowAsync(info, NotificationTypes.Ask, isTopmost);
        }
        public static async Task<bool> Warn(string info, bool isTopmost = true)
        {
            return await OpenWindowAsync(info, NotificationTypes.Warn, isTopmost);
        }
        public static async Task Message(string info, bool isTopmost = true)
        {
            await OpenWindowAsync(info, NotificationTypes.Message, isTopmost);
        }

        private static async Task<bool> OpenWindowAsync(string info, NotificationTypes type, bool isTopmost)
        {
            Application.Current.MainWindow.IsEnabled = false;
            _tcs = new TaskCompletionSource<bool>();
            Instance.Text = info;
            Instance.NotificationType = type;
            Instance.Topmost = isTopmost;
            Application.Current.MainWindow.Closed += Instance.CloseLink;

            Instance.Opacity = 1;

            bool result = await _tcs.Task;
            return result;
        }

        private void CloseLink(object? sender, EventArgs e)
        {
            Close();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _tcs?.SetResult(true);
            Opacity = 0.0000001;
            Application.Current.MainWindow.IsEnabled = true;
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            _tcs?.SetResult(false);
            Opacity = 0.0000001;
            Application.Current.MainWindow.IsEnabled = true;
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Opacity = 0.0000001;
            Application.Current.MainWindow.IsEnabled = true;
        }
        private void TextBlock_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}