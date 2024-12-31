using MinimalisticWPF.Animator;
using System.Windows;
using System.Windows.Media;

namespace MinimalisticWPF.Controls.ViewModel
{
    public partial class ButtonViewModel
    {
        [Observable]
        private string _text = string.Empty;

        [Constructor]
        private void SetInitialTheme()
        {
            CurrentTheme = typeof(Dark);
            var darkcolor = "#1e1e1e".ToBrush();
            HoveredBorderThickness = new(1.25);
            HoveredCornerRadius = new(5);
            DarkHoveredForeground = Brushes.Cyan;
            DarkHoveredBackground = Brushes.Transparent;
            DarkHoveredBorderBrush = Brushes.White;

            LightHoveredForeground = Brushes.Violet;
            LightHoveredBackground = Brushes.Transparent;
            LightHoveredBorderBrush = darkcolor;

            HoveredTransition.SetParams(TransitionParams.Hover);
            NoHoveredTransition.SetParams(TransitionParams.Hover);
        }

        [Observable]
        private bool _isHover = false;
        partial void OnIsHoverChanged(bool oldValue, bool newValue)
        {
            if (!IsThemeChanging)
            {
                UpdateState();
            }
        }

        [Observable(CanHover: true)]
        private Thickness _borderThickness = new(0);
        partial void OnHoveredBorderThicknessChanged(Thickness oldValue, Thickness newValue)
        {
            HoveredTransition.SetProperty(b => b.BorderThickness, newValue);
        }
        partial void OnNoHoveredBorderThicknessChanged(Thickness oldValue, Thickness newValue)
        {
            NoHoveredTransition.SetProperty(b => b.BorderThickness, newValue);
        }

        [Observable(CanHover: true)]
        private CornerRadius _cornerRadius = new(0);
        partial void OnHoveredCornerRadiusChanged(CornerRadius oldValue, CornerRadius newValue)
        {
            HoveredTransition.SetProperty(b => b.CornerRadius, newValue);
        }
        partial void OnNoHoveredCornerRadiusChanged(CornerRadius oldValue, CornerRadius newValue)
        {
            NoHoveredTransition.SetProperty(b => b.CornerRadius, newValue);
        }

        [Observable(CanHover: true)]
        [Dark("White")]
        [Light("#1e1e1e")]
        private Brush _borderBrush = Brushes.White;
        partial void OnDarkHoveredBorderBrushChanged(Brush oldValue, Brush newValue)
        {
            if (CurrentTheme != typeof(Dark)) return;
            HoveredTransition.SetProperty(b => b.BorderBrush, newValue);
        }
        partial void OnDarkNoHoveredBorderBrushChanged(Brush oldValue, Brush newValue)
        {
            if (CurrentTheme != typeof(Dark)) return;
            NoHoveredTransition.SetProperty(b => b.BorderBrush, newValue);
        }
        partial void OnLightHoveredBorderBrushChanged(Brush oldValue, Brush newValue)
        {
            if (CurrentTheme != typeof(Light)) return;
            HoveredTransition.SetProperty(b => b.BorderBrush, newValue);
        }
        partial void OnLightNoHoveredBorderBrushChanged(Brush oldValue, Brush newValue)
        {
            if (CurrentTheme != typeof(Light)) return;
            NoHoveredTransition.SetProperty(b => b.BorderBrush, newValue);
        }

        [Observable(CanHover: true)]
        [Dark("White")]
        [Light("#1e1e1e")]
        private Brush _foreground = Brushes.White;
        partial void OnDarkHoveredForegroundChanged(Brush oldValue, Brush newValue)
        {
            if (CurrentTheme != typeof(Dark)) return;
            HoveredTransition.SetProperty(b => b.Foreground, newValue);
        }
        partial void OnDarkNoHoveredForegroundChanged(Brush oldValue, Brush newValue)
        {
            if (CurrentTheme != typeof(Dark)) return;
            NoHoveredTransition.SetProperty(b => b.Foreground, newValue);
        }
        partial void OnLightHoveredForegroundChanged(Brush oldValue, Brush newValue)
        {
            if (CurrentTheme != typeof(Light)) return;
            HoveredTransition.SetProperty(b => b.Foreground, newValue);
        }
        partial void OnLightNoHoveredForegroundChanged(Brush oldValue, Brush newValue)
        {
            if (CurrentTheme != typeof(Light)) return;
            NoHoveredTransition.SetProperty(b => b.Foreground, newValue);
        }

        [Observable(CanHover: true)]
        [Dark("Transparent")]
        [Light("Transparent")]
        private Brush _background = Brushes.Transparent;
        partial void OnDarkHoveredBackgroundChanged(Brush oldValue, Brush newValue)
        {
            if (CurrentTheme != typeof(Dark)) return;
            HoveredTransition.SetProperty(b => b.Background, newValue);
        }
        partial void OnDarkNoHoveredBackgroundChanged(Brush oldValue, Brush newValue)
        {
            if (CurrentTheme != typeof(Dark)) return;
            NoHoveredTransition.SetProperty(b => b.Background, newValue);
        }
        partial void OnLightHoveredBackgroundChanged(Brush oldValue, Brush newValue)
        {
            if (CurrentTheme != typeof(Light)) return;
            HoveredTransition.SetProperty(b => b.Background, newValue);
        }
        partial void OnLightNoHoveredBackgroundChanged(Brush oldValue, Brush newValue)
        {
            if (CurrentTheme != typeof(Light)) return;
            NoHoveredTransition.SetProperty(b => b.Background, newValue);
        }

        public partial void OnThemeChanging(Type? oldTheme, Type newTheme)
        {
            Transition.DisposeSafe(this);
        }
        public partial void OnThemeChanged(Type? oldTheme, Type newTheme)
        {
            var isdark = CurrentTheme == typeof(Dark);

            HoveredTransition.SetProperty(b => b.Foreground, isdark ? DarkHoveredForeground : DarkNoHoveredForeground);
            HoveredTransition.SetProperty(b => b.Background, isdark ? DarkHoveredBackground : DarkNoHoveredBackground);
            HoveredTransition.SetProperty(b => b.BorderBrush, isdark ? DarkHoveredBorderBrush : DarkNoHoveredBorderBrush);

            NoHoveredTransition.SetProperty(b => b.Foreground, isdark ? DarkHoveredForeground : DarkNoHoveredForeground);
            NoHoveredTransition.SetProperty(b => b.Background, isdark ? DarkHoveredBackground : DarkNoHoveredBackground);
            NoHoveredTransition.SetProperty(b => b.BorderBrush, isdark ? DarkHoveredBorderBrush : DarkNoHoveredBorderBrush);

            UpdateState();
        }

        private void UpdateState()
        {
            var isdark = CurrentTheme == typeof(Dark);

            HoveredTransition.SetProperty(b => b.Foreground, isdark ? DarkHoveredForeground : LightHoveredForeground);
            HoveredTransition.SetProperty(b => b.Background, isdark ? DarkHoveredBackground : LightHoveredBackground);
            HoveredTransition.SetProperty(b => b.BorderBrush, isdark ? DarkHoveredBorderBrush : LightHoveredBorderBrush);

            NoHoveredTransition.SetProperty(b => b.Foreground, isdark ? DarkNoHoveredForeground : LightNoHoveredForeground);
            NoHoveredTransition.SetProperty(b => b.Background, isdark ? DarkNoHoveredBackground : LightNoHoveredBackground);
            NoHoveredTransition.SetProperty(b => b.BorderBrush, isdark ? DarkNoHoveredBorderBrush : LightNoHoveredBorderBrush);

            this.BeginTransition(IsHover ? HoveredTransition : NoHoveredTransition);
        }
    }
}
