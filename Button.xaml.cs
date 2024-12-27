using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MinimalisticWPF.Controls
{
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

        public Brush DarkHoverBackground
        {
            get => ViewModel.DarkHoverBackground;
            set => ViewModel.DarkHoverBackground = value;
        }

        public Brush LightHoverBackground
        {
            get => ViewModel.LightHoverBackground;
            set => ViewModel.LightHoverBackground = value;
        }

        private int _clickdowntime = 0;
        private int _clickuptime = 0;
        public event RoutedEventHandler? Click;

        private void Root_MouseEnter(object sender, MouseEventArgs e)
        {
            ViewModel.IsHover = true;
        }

        private void Root_MouseLeave(object sender, MouseEventArgs e)
        {
            ViewModel.IsHover = false;
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
