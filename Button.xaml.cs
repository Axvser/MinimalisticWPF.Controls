using MinimalisticWPF.Controls.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace MinimalisticWPF.Controls
{
    [DataContextConfig(nameof(ButtonViewModel))]
    public partial class Button : UserControl
    {
        public Button()
        {
            InitializeComponent();
        }

        public string Text
        {
            get => ViewModel.Text;
            set => ViewModel.Text = value;
        }

        private int _clickdowntime = 0;
        private int _clickuptime = 0;
        public event RoutedEventHandler? Click;

        private void Root_MouseEnter(object sender, MouseEventArgs e)
        {
            ViewModel.IsHovered = true;
        }
        private void Root_MouseLeave(object sender, MouseEventArgs e)
        {
            ViewModel.IsHovered = false;
            _clickdowntime = 0;
            _clickuptime = 0;
        }
        private void Root_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _clickdowntime++;
            CheckIfClick(e);
        }
        private void Root_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _clickuptime++;
            CheckIfClick(e);
        }
        private void CheckIfClick(MouseButtonEventArgs e)
        {
            if (_clickdowntime > 0 && _clickuptime > 0)
            {
                Click?.Invoke(this, e);
                _clickdowntime = 0;
                _clickuptime = 0;
            }
        }
    }
}
