using MinimalisticWPF.Animator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;

namespace MinimalisticWPF.Controls.ViewModel
{
    public partial class ButtonViewModel
    {
        [Constructor]
        private void SetInitialTheme()
        {
            CurrentTheme = typeof(Dark);

            HoveredTransition.SetParams(TransitionParams.Hover);
            NoHoveredTransition.SetParams(TransitionParams.Hover);
        }

        [Observable(CanHover: true)]
        private Thickness _borderThickness = new Thickness(0);

        [Observable(CanHover: true)]
        private CornerRadius _cornerRadius = new CornerRadius(0);

        [Observable(CanHover: true)]
        [Dark("White")]
        [Light("#1e1e1e")]
        private Brush _borderBrush = Brushes.White;

        [Observable(CanHover: true)]
        [Dark("White")]
        [Light("#1e1e1e")]
        private Brush _foreground = Brushes.White;

        [Observable(CanHover: true)]
        [Dark("Transparent")]
        [Light("Transparent")]
        private Brush _background = Brushes.Transparent;

        public partial void OnThemeChanging(Type? oldTheme, Type newTheme)
        {
            Transition.DisposeSafe(this);
        }
        public partial void OnThemeChanged(Type? oldTheme, Type newTheme)
        {
            UpdateTransitionBoard();
        }
    }
}
