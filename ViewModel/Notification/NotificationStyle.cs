using MinimalisticWPF.Theme;
using MinimalisticWPF.TransitionSystem;
using System.Windows.Media;
using System.Windows;

namespace MinimalisticWPF.Controls.ViewModel
{
    public partial class Notification
    {
        [Constructor]
        private void SetInitialValue()
        {
            CurrentTheme = typeof(Dark);
            HoveredTransition.SetParams(TransitionParams.Hover);
            NoHoveredTransition.SetParams(TransitionParams.Hover);
        }

        [Observable(CanDependency:true)]
        private string text = string.Empty;

        [Observable(CanHover: true)]
        private Thickness _borderThickness = new Thickness(0);

        [Observable(CanHover: true)]
        private CornerRadius _cornerRadius = new CornerRadius(0);

        [Observable(CanHover: true, CanIsolated: true)]
        [Dark("White")]
        [Light("#1e1e1e")]
        private Brush _borderBrush = Brushes.White;

        [Observable(CanHover: true, CanIsolated: true)]
        [Dark("White")]
        [Light("Black")]
        private Brush _foreground = Brushes.White;

        [Observable(CanHover: true, CanIsolated: true)]
        [Dark("#BB000000")]
        [Light("#DDFFFFFF")]
        private Brush _background = RGB.FromString("#AA000000").Brush;

        [Observable]
        private string _title = "Message";

        [Observable]
        private Visibility _visibilityMakeSure = Visibility.Collapsed;
        [Observable]
        private Visibility _visibilityMessage = Visibility.Visible;

        [Observable(CanDependency: true)]
        private NotificationTypes _notificationType = NotificationTypes.Message;
        partial void OnNotificationTypeChanged(NotificationTypes oldValue, NotificationTypes newValue)
        {
            switch (newValue)
            {
                case NotificationTypes.Message:
                    Title = "Message";
                    VisibilityMessage = Visibility.Visible;
                    VisibilityMakeSure = Visibility.Collapsed;
                    break;
                case NotificationTypes.Ask:
                    Title = "Ask";
                    VisibilityMessage = Visibility.Collapsed;
                    VisibilityMakeSure = Visibility.Visible;
                    break;
                case NotificationTypes.Warn:
                    Title = "⚠ Warn";
                    VisibilityMessage = Visibility.Collapsed;
                    VisibilityMakeSure = Visibility.Visible;
                    break;
            }
        }
    }
}
