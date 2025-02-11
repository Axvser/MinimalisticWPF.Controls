using System.Windows.Media;
using System.Windows;
using MinimalisticWPF.Theme;
using MinimalisticWPF.TransitionSystem;

namespace MinimalisticWPF.Controls.ViewModel
{
    public partial class Button
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

        [Observable(CanHover: true, CanIsolated: true)]
        [Dark("White")]
        [Light("#1e1e1e")]
        private Brush _borderBrush = Brushes.White;

        [Observable(CanHover: true, CanIsolated: true)]
        [Dark("White")]
        [Light("#1e1e1e")]
        private Brush _foreground = Brushes.White;

        [Observable(CanHover: true, CanIsolated: true)]
        [Dark("Transparent")]
        [Light("Transparent")]
        private Brush _background = Brushes.Transparent;

        [Observable(CanHover: true, SetterValidation: SetterValidations.Custom)]
        private double _backOpacity = (double)1;
        private partial bool BackOpacityIntercepting(double oldValue, double newValue)
        {
            return newValue < 0;
        }

        partial void OnThemeChanging(Type? oldTheme, Type newTheme)
        {
            Transition.Dispose(this);
        }
        partial void OnThemeChanged(Type? oldTheme, Type newTheme)
        {
            UpdateTransitionBoard();
        }
    }
}
