using MinimalisticWPF.SourceGeneratorMark;
using MinimalisticWPF.Theme;
using MinimalisticWPF.Tools.SolidColor;
using MinimalisticWPF.TransitionSystem;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MinimalisticWPF.Controls
{
    [MonoBehaviour(16)]
    public partial class FlowingCard : Grid
    {
        // 控件初始化
        [Constructor]
        private void InitializeShadow()
        {
            CanMonoBehaviour = false;
            MouseEnter += (s, e) => CanMonoBehaviour = true;
            MouseLeave += (s, e) => CanMonoBehaviour = false;
        }

        // MonoBehaviour 生命周期
        partial void Start()
        {
            this.BeginTransition(BlurToFull);
        }
        partial void Update()
        {
            ShadowAngle += 3;
            UpdateShadowAngle(ShadowAngle);
        }
        partial void LateUpdate()
        {
            if (ShadowAngle > 360)
            {
                ShadowAngle %= 360;
            }
        }
        partial void ExitMonoBehaviour()
        {
            this.BeginTransition(ShadowAngle > 225 ? AngleToFull : AngleToTan);
        }

        public Brush ShadowBrush
        {
            get { return (LinearGradientBrush)GetValue(ShadowBrushProperty); }
            set { SetValue(ShadowBrushProperty, value); }
        } // 阴影画刷
        public static readonly DependencyProperty ShadowBrushProperty =
            DependencyProperty.Register("ShadowBrush", typeof(LinearGradientBrush), typeof(FlowingCard), new PropertyMetadata(new LinearGradientBrush()
            {
                StartPoint = new Point(0, 0),
                EndPoint = new Point(1, 1),
                GradientStops = [new GradientStop(Colors.Cyan, 0), new GradientStop(Colors.Blue, 1)]
            }));

        public double BlurRadius
        {
            get { return (double)GetValue(BlurRadiusProperty); }
            set { SetValue(BlurRadiusProperty, value); }
        } // 模糊大小
        public static readonly DependencyProperty BlurRadiusProperty =
            DependencyProperty.Register("BlurRadius", typeof(double), typeof(FlowingCard), new PropertyMetadata(0d));

        public double ShadowAngle
        {
            get { return (double)GetValue(ShadowAngleProperty); }
            set { SetValue(ShadowAngleProperty, value); }
        } // 旋转角
        public static readonly DependencyProperty ShadowAngleProperty =
            DependencyProperty.Register("ShadowAngle", typeof(double), typeof(FlowingCard), new PropertyMetadata(45d));

        public void UpdateShadowAngle(double angle)
        {
            double radians = angle * Math.PI / 180d;
            double x = Math.Cos(radians);
            double y = Math.Sin(radians);
            var newStartPoint = new Point(0.5 - x * 0.5, 0.5 - y * 0.5);
            var newEndPoint = new Point(0.5 + x * 0.5, 0.5 + y * 0.5);
            ShadowBrush = new LinearGradientBrush()
            {
                StartPoint = newStartPoint,
                EndPoint = newEndPoint,
                GradientStops = [new GradientStop(((SolidColorBrush)ShadowStartBrush).Color, 0), new GradientStop(((SolidColorBrush)ShadowEndBrush).Color, 1)]
            };
        } // 依据旋转角刷新渐变画刷
    }

    public partial class FlowingCard
    {
        public TransitionBoard<FlowingCard> BlurToFull = Transition.Create<FlowingCard>()
            .SetProperty(card => card.BlurRadius, 100d)
            .SetParams((p) =>
            {
                p.Duration = 2;
            }); // 逐渐显现光

        public TransitionBoard<FlowingCard> AngleToTan = Transition.Create<FlowingCard>()
            .SetProperty(card => card.ShadowAngle, 45d)
            .SetProperty(card => card.BlurRadius, 0d)
            .SetParams((p) =>
            {
                p.Duration = 0.3;
                p.LateUpdate += (s, e) =>
                {
                    if (s is FlowingCard card)
                    {
                        card.UpdateShadowAngle(card.ShadowAngle);
                    }
                };
            }); // 逆时针回转

        public TransitionBoard<FlowingCard> AngleToFull = Transition.Create<FlowingCard>()
            .SetProperty(card => card.ShadowAngle, 405d)
            .SetProperty(card => card.BlurRadius, 0d)
            .SetParams((p) =>
            {
                p.Duration = 0.3;
                p.LateUpdate += (s, e) =>
                {
                    if (s is FlowingCard card)
                    {
                        card.UpdateShadowAngle(card.ShadowAngle);
                    }
                };
            }); // 顺时针回转
    } // 动画

    [Theme(nameof(ShadowStartBrush), typeof(Dark), [nameof(dark_startbrush)])]
    [Theme(nameof(ShadowStartBrush), typeof(Light), [nameof(light_startbrush)])]
    [Theme(nameof(ShadowEndBrush), typeof(Dark), [nameof(dark_endbrush)])]
    [Theme(nameof(ShadowEndBrush), typeof(Light), [nameof(light_endbrush)])]
    [Theme(nameof(Background), typeof(Dark), ["#1e1e1e"])]
    [Theme(nameof(Background), typeof(Light), ["White"])]
    public partial class FlowingCard
    {
        public Thickness BorderThickness
        {
            get { return (Thickness)GetValue(BorderThicknessorderThicknessProperty); }
            set { SetValue(BorderThicknessorderThicknessProperty, value); }
        }
        public static readonly DependencyProperty BorderThicknessorderThicknessProperty =
            DependencyProperty.Register("BorderThickness", typeof(Thickness), typeof(FlowingCard), new PropertyMetadata(new Thickness(2)));

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(FlowingCard), new PropertyMetadata(new CornerRadius(5)));

        public Brush ShadowStartBrush
        {
            get { return (Brush)GetValue(ShadowStartBrushProperty); }
            set { SetValue(ShadowStartBrushProperty, value); }
        }
        public static readonly DependencyProperty ShadowStartBrushProperty =
            DependencyProperty.Register("ShadowStartBrush", typeof(Brush), typeof(FlowingCard), new PropertyMetadata(Brushes.Transparent, (s, e) =>
            {
                if (s is FlowingCard card && card.ShadowBrush is LinearGradientBrush brush)
                {
                    var newBrush = new LinearGradientBrush()
                    {
                        StartPoint = brush.StartPoint,
                        EndPoint = brush.EndPoint,
                        GradientStops = [new GradientStop(((SolidColorBrush)e.NewValue).Color, 0), brush.GradientStops[1]]
                    };
                    card.ShadowBrush = newBrush;
                }
            }));

        public Brush ShadowEndBrush
        {
            get { return (Brush)GetValue(ShadowEndBrushProperty); }
            set { SetValue(ShadowEndBrushProperty, value); }
        }
        public static readonly DependencyProperty ShadowEndBrushProperty =
            DependencyProperty.Register("ShadowEndBrush", typeof(Brush), typeof(FlowingCard), new PropertyMetadata(Brushes.Transparent, (s, e) =>
            {
                if (s is FlowingCard card && card.ShadowBrush is LinearGradientBrush brush)
                {
                    var newBrush = new LinearGradientBrush()
                    {
                        StartPoint = brush.StartPoint,
                        EndPoint = brush.EndPoint,
                        GradientStops = [brush.GradientStops[0], new GradientStop(((SolidColorBrush)e.NewValue).Color, 1)]
                    };
                    card.ShadowBrush = newBrush;
                }
            }));

        public static readonly SolidColorBrush dark_startbrush = RGB.FromString("Cyan").SolidColorBrush;
        public static readonly SolidColorBrush dark_endbrush = RGB.FromString("Blue").SolidColorBrush;
        public static readonly SolidColorBrush light_startbrush = RGB.FromString("Lime").SolidColorBrush;
        public static readonly SolidColorBrush light_endbrush = RGB.FromString("Cyan").SolidColorBrush;
    } // 其它上下文
}
