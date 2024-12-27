using MinimalisticWPF.Animator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace MinimalisticWPF.Controls.ViewModel
{
    [Theme]
    public partial class ButtonVM : MinimalisticControlViewModel
    {
        [VMProperty]
        private string _text = "Button";
        [VMProperty]
        [Dark("White")]
        [Light("#1e1e1e")]
        private Brush _textBrush = Brushes.White;

        [VMProperty]
        private CornerRadius _cornerRadius = new(0);
        [VMProperty]
        private double _borderOpacity = 0;
        [VMProperty]
        [Dark("White")]
        [Light("#1e1e1e")]
        private Brush _borderBrush = Brushes.White;
        [VMProperty]
        private Thickness _borderThickness = new(0);

        [VMProperty]
        private Brush _background = Brushes.Transparent;

        private Brush _darkHoverbackground = Brushes.Gray.ToRGB().SubA(100).Brush;
        public virtual Brush DarkHoverBackground
        {
            get => _darkHoverbackground;
            set
            {
                if (_darkHoverbackground != value)
                {
                    _darkHoverbackground = value;
                    if (NowTheme == typeof(Dark) || NowTheme == null)
                    {
                        Selected.SetProperty(x => x.Background, value);
                    }
                }
            }
        }

        private Brush _lightHoverbackground = new RGB(255, 255, 255, 100).Brush;
        public virtual Brush LightHoverBackground
        {
            get => _lightHoverbackground;
            set
            {
                if (_lightHoverbackground != value)
                {
                    _lightHoverbackground = value;
                    if (NowTheme == typeof(Light))
                    {
                        Selected.SetProperty(x => x.Background, value);
                    }
                }
            }
        }


        private bool _isHover = false;
        public virtual bool IsHover
        {
            get => _isHover;
            set
            {
                if (_isHover != value)
                {
                    _isHover = value;
                    if (!_isThemeChanging)
                    {
                        this.BeginTransition(value ? Selected : NoSelected, ParamAction);
                    }
                }
            }
        }

        private bool _isThemeChanging = false;

        public virtual TransitionBoard<ButtonVM> Selected { get; protected set; } = Transition.Create<ButtonVM>()
            .SetProperty(b => b.BorderOpacity, 1)
            .SetProperty(b => b.BorderThickness, new Thickness(1.5))
            .SetProperty(b => b.CornerRadius, new CornerRadius(5))
            .SetProperty(b => b.Background, new RGB(255, 255, 255, 100).Brush);

        public virtual TransitionBoard<ButtonVM> NoSelected { get; protected set; } = Transition.Create<ButtonVM>()
            .SetProperty(b => b.BorderOpacity, 0)
            .SetProperty(b => b.BorderThickness, new Thickness(0))
            .SetProperty(b => b.CornerRadius, new CornerRadius(0));

        [BeforeThemeChanged]
        public virtual void StopHoverAnimation()
        {
            _isThemeChanging = true;
            Transition.DisposeSafe(this);
            if (_isHover)
            {
                Background = NowTheme == typeof(Light) ? LightHoverBackground : DarkHoverBackground;
            }
        }

        [AfterThemeChanged]
        public virtual void ContinueHoverAnimation()
        {
            Selected.SetProperty(x => x.BorderBrush, NowTheme == typeof(Dark) ? Brushes.White : "#1e1e1e".ToBrush());
            NoSelected.SetProperty(x => x.BorderBrush, NowTheme == typeof(Dark) ? Brushes.White : "#1e1e1e".ToBrush());

            Selected.SetProperty(x => x.TextBrush, NowTheme == typeof(Dark) ? Brushes.White : "#1e1e1e".ToBrush());
            NoSelected.SetProperty(x => x.TextBrush, NowTheme == typeof(Dark) ? Brushes.White : "#1e1e1e".ToBrush());

            Selected.SetProperty(x => x.Background, NowTheme == typeof(Dark) ? LightHoverBackground : DarkHoverBackground);

            this.BeginTransition(_isHover ? Selected : NoSelected, ParamAction);
            _isThemeChanging = false;
        }
    }
}
