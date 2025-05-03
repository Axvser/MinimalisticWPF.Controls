using MinimalisticWPF.SourceGeneratorMark;
using MinimalisticWPF.Theme;
using MinimalisticWPF.Tools.SVG;
using MinimalisticWPF.TransitionSystem;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MinimalisticWPF.Controls
{
    [ClickModule]
    [Hover([nameof(Background), nameof(Foreground)])]
    [Hover([nameof(BorderBrush), nameof(BorderThickness), nameof(CornerRadius)])]
    public partial class Button : UserControl
    {
        [Constructor]
        private void InitializeGeomertyOrText()
        {
            Loaded += (s, e) => UpdateGeometryOrText();
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            UpdateGeometryOrText();
        }
    } // 默认交互

    public partial class Button
    {
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(Button), new PropertyMetadata(default(CornerRadius)));

        public string Data
        {
            get { return (string)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }
        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register("Data", typeof(string), typeof(Button), new PropertyMetadata(string.Empty, (dp, e) =>
            {
                if (dp is Button button)
                {
                    button.UpdateGeometryOrText();
                }
            }));

        public double ContentScale
        {
            get { return (double)GetValue(ContentScaleProperty); }
            set { SetValue(ContentScaleProperty, value); }
        }
        public static readonly DependencyProperty ContentScaleProperty =
            DependencyProperty.Register("ContentScale", typeof(double), typeof(Button), new PropertyMetadata(1d, (dp, e) =>
            {
                if (dp is Button button)
                {
                    button.UpdateGeometryOrText();
                }
            }));

        internal string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        internal static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(Button), new PropertyMetadata(string.Empty));

        internal Geometry SVG
        {
            get { return (Geometry)GetValue(SVGProperty); }
            set { SetValue(SVGProperty, value); }
        }
        internal static readonly DependencyProperty SVGProperty =
            DependencyProperty.Register("SVG", typeof(Geometry), typeof(Button), new PropertyMetadata(Geometry.Empty));

        public bool AllowHoverRotate
        {
            get { return (bool)GetValue(AllowHoverRotateProperty); }
            set { SetValue(AllowHoverRotateProperty, value); }
        }
        public static readonly DependencyProperty AllowHoverRotateProperty =
            DependencyProperty.Register("AllowHoverRotate", typeof(bool), typeof(Button), new PropertyMetadata(false));

        internal Transform ChildTransform
        {
            get { return (Transform)GetValue(ChildTransformProperty); }
            set { SetValue(ChildTransformProperty, value); }
        }
        internal static readonly DependencyProperty ChildTransformProperty =
            DependencyProperty.Register("ChildTransform", typeof(Transform), typeof(Button), new PropertyMetadata(Transform.Identity));

        /// <summary>
        /// 尝试解析 Data 属性为 Geometry 对象，若失败，则将 Data 作为文本显示。
        /// <para>注意: 此举将基于 Min(宽,高) * ScaleRate 动态计算字体大小与SVG图大小</para>
        /// </summary>
        public void UpdateGeometryOrText()
        {
            var size = Math.Min(ActualWidth, ActualHeight) * ContentScale;
            FontSize = size <= 0 ? 1 : size;
            try
            {
                var geometry = Geometry.Parse(Data);
                Text = string.Empty;
                SVG = geometry.Adapt(this);
            }
            catch (FormatException)
            {
                Text = Data;
                SVG = Geometry.Empty;
            }
        }
    } // 依赖属性

    [MonoBehaviour(16)]
    public partial class Button
    {
        /* 60FPS 旋转动画
         * 1. 旋转中心为控件的中心  
         * 2. 悬停时开始旋转
         * 3. 鼠标离开时停止旋转，且角度需要回正
         */

        private readonly RotateTransform rotate = new();

        [Constructor]
        private void ApplyRotate()
        {
            MouseEnter += (s, e) => CanMonoBehaviour = true;
            MouseLeave += (s, e) => CanMonoBehaviour = false;
            Loaded += (s, e) =>
            {
                UpdateRotateCenter();
                ChildTransform = rotate;
            };
            SizeChanged += (s, e) =>
            {
                UpdateRotateCenter();
            };
        }

        partial void Awake()
        {
            CanMonoBehaviour = false;
        }
        partial void Start()
        {
            Transition.Dispose(rotate);
        }
        partial void Update()
        {
            if (AllowHoverRotate)
            {
                var newAngle = (rotate.Angle + 2) % 360;
                rotate.Angle = newAngle;
            }
        }
        partial void ExitMonoBehaviour()
        {
            rotate.Transition()
                .SetProperty(x => x.Angle, (360 - rotate.Angle) < 180 ? 360 : 0)
                .SetParams((p) =>
                {
                    p.Duration = 0.3;
                    p.Completed += () =>
                    {
                        rotate.Angle = 0;
                    };
                })
                .Start();
        }

        private void UpdateRotateCenter()
        {
            var center = new Point(ActualWidth / 2, ActualHeight / 2);
            rotate.CenterX = center.X;
            rotate.CenterY = center.Y;
        }
    } // 实时刷新

    [Theme(nameof(Background), typeof(Light), [])]
    [Theme(nameof(Background), typeof(Dark), [])]
    [Theme(nameof(BorderBrush), typeof(Light), [])]
    [Theme(nameof(BorderBrush), typeof(Dark), [])]
    [Theme(nameof(Foreground), typeof(Light), [])]
    [Theme(nameof(Foreground), typeof(Dark), [])]
    public partial class Button
    {

    } // 外观效果
}
