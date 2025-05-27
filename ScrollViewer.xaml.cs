using MinimalisticWPF.HotKey;
using MinimalisticWPF.SourceGeneratorMark;
using MinimalisticWPF.Theme;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace MinimalisticWPF.Controls
{
    public partial class ScrollViewer : System.Windows.Controls.ScrollViewer
    {
        private void Viewer_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
            {
                if (ContentScale.Children[0] is ScaleTransform scaleTransform)
                {
                    double delta = e.Delta > 0 ? 0.1 : -0.1;
                    double newScaleX = Math.Clamp(scaleTransform.ScaleX + delta, 0, double.MaxValue);
                    double newScaleY = Math.Clamp(scaleTransform.ScaleY + delta, 0, double.MaxValue);

                    ContentScale.Children[0] = new ScaleTransform(newScaleX, newScaleY);

                    e.Handled = true;
                }
            }
        }

        private void Left()
        {
            ScrollToHorizontalOffset(HorizontalOffset - ViewportWidth * 0.2);
        }
        private void Right()
        {
            ScrollToHorizontalOffset(HorizontalOffset + ViewportWidth * 0.2);
        }
        private void Up()
        {
            ScrollToVerticalOffset(VerticalOffset + ViewportHeight * 0.2);
        }
        private void Down()
        {
            ScrollToVerticalOffset(VerticalOffset - ViewportHeight * 0.2);
        }

        private void Left(object sender, KeyEventArgs e)
        {
            Left();
        }
        private void Right(object sender, KeyEventArgs e)
        {
            Right();
        }
        private void Up(object sender, KeyEventArgs e)
        {
            Up();
        }
        private void Down(object sender, KeyEventArgs e)
        {
            Down();
        }

        private void Left(object sender, RoutedEventArgs e)
        {
            Left();
        }
        private void Right(object sender, RoutedEventArgs e)
        {
            Right();
        }
        private void Up(object sender, RoutedEventArgs e)
        {
            Up();
        }
        private void Down(object sender, RoutedEventArgs e)
        {
            Down();
        }
    }

    [Theme(nameof(Background), typeof(Dark), ["#01ffffff"])]
    [Theme(nameof(Background), typeof(Light), ["#01ffffff"])]
    [Theme(nameof(Foreground), typeof(Dark), ["White"])]
    [Theme(nameof(Foreground), typeof(Light), ["#1e1e1e"])]
    public partial class ScrollViewer
    {
        [Constructor]
        private void InitializeScale()
        {
            ContentScale = new TransformGroup();
            ContentScale.Children.Add(new ScaleTransform(1, 1));
            LocalHotKey.Register(this, [Key.Left], Left);
            LocalHotKey.Register(this, [Key.Right], Right);
            LocalHotKey.Register(this, [Key.Up], Up);
            LocalHotKey.Register(this, [Key.Down], Down);
        }

        public TransformGroup ContentScale
        {
            get { return (TransformGroup)GetValue(ContentScaleProperty); }
            set { SetValue(ContentScaleProperty, value); }
        }
        public static readonly DependencyProperty ContentScaleProperty =
            DependencyProperty.Register("ContentScale", typeof(TransformGroup), typeof(ScrollViewer),
            new PropertyMetadata(new TransformGroup()));
    }
}
