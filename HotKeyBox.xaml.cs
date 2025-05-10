using MinimalisticWPF.HotKey;
using MinimalisticWPF.SourceGeneratorMark;
using MinimalisticWPF.Theme;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MinimalisticWPF.Controls
{
    [HotKeyComponent]
    public partial class HotKeyBox : UserControl
    {
        [Constructor]
        private void InitializeTextSize()
        {
            Loaded += (s, e) => UpdateTextSize();
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            UpdateTextSize();
        }

        partial void OnHotKeyUpdated() // 热键更新后,文本也更新
        {
            Text = key == 0x0000 ?
                string.Join(" > ", [.. HotKeyHelper.GetNames(modifiers)]) :
                string.Join(" > ", [.. HotKeyHelper.GetNames(modifiers), key.ToString()]);
        }
        partial void OnHotKeyCovered() // 如果其它Box内出现与此Box相同的热键,此Box注册的热键会被覆盖,此时清空文本
        {
            Text = string.Empty;
        }

        private void TextBox_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is TextBox box)
            {
                box.Focusable = true;
                box.Focus();
            }
        }
        private void TextBox_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is TextBox box)
            {
                box.Focusable = false;
                Keyboard.ClearFocus();
            }
        }
    }

    public partial class HotKeyBox
    {
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(HotKeyBox), new PropertyMetadata(default(CornerRadius)));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(HotKeyBox), new PropertyMetadata(string.Empty));

        public double TextScale
        {
            get { return (double)GetValue(TextScaleProperty); }
            set { SetValue(TextScaleProperty, value); }
        }
        public static readonly DependencyProperty TextScaleProperty =
            DependencyProperty.Register("TextScale", typeof(double), typeof(HotKeyBox), new PropertyMetadata(0.618d, (dp, e) =>
            {
                if (dp is HotKeyBox hotKeyBox)
                {
                    hotKeyBox.UpdateTextSize();
                }
            }));

        public void UpdateTextSize()
        {
            var size = Math.Min(ActualWidth, ActualHeight) * TextScale;
            FontSize = size <= 0 ? 1 : size;
        }
    } // DependencyProperty

    [Theme(nameof(Background), typeof(Light), [])]
    [Theme(nameof(Background), typeof(Dark), [])]
    [Theme(nameof(BorderBrush), typeof(Light), [])]
    [Theme(nameof(BorderBrush), typeof(Dark), [])]
    [Theme(nameof(Foreground), typeof(Light), [])]
    [Theme(nameof(Foreground), typeof(Dark), [])]
    [Hover([nameof(Background), nameof(Foreground)])]
    [Hover([nameof(BorderBrush), nameof(BorderThickness), nameof(CornerRadius)])]
    public partial class HotKeyBox
    {

    } // Effect
}
