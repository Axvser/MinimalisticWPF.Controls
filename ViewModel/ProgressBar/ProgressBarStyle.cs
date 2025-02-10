using MinimalisticWPF.Theme;
using MinimalisticWPF.TransitionSystem;
using System.Windows;
using System.Windows.Media;

namespace MinimalisticWPF.Controls.ViewModel
{
    public partial class ProgressBar
    {
        [Constructor]
        private void SetInitialTheme()
        {
            CurrentTheme = typeof(Dark);
        }

        [Observable]
        private double barWidth = 200;
        [Observable]
        private double barHeight = 200;

        [Observable]
        private Visibility ringVisibility = Visibility.Visible;
        [Observable]
        private Visibility lineVisibility = Visibility.Collapsed;

        [Observable(CanDependency: true)]
        private ProgressBarShape shape = ProgressBarShape.Ring;

        [Observable(CanDependency: true)]
        private double size = (double)200;

        [Observable(CanDependency: true)]
        private double _thickness = (double)3;

        [Observable(CanDependency: true)]
        [Dark(nameof(Brushes.Cyan))]
        [Light(nameof(Brushes.Violet))]
        private Brush foreFill = Brushes.Cyan;
        [Observable(CanDependency: true)]
        [Dark(nameof(Brushes.Gray))]
        [Light(nameof(Brushes.Gray))]
        private Brush backFill = Brushes.Gray;

        [Observable(CanDependency: true)]
        private bool flipX = false;
        [Observable(CanDependency: true)]
        private bool flipY = false;

        [Observable]
        private double scaleX = 1;
        [Observable]
        private double scaleY = 1;
        [Observable]
        private double transX = 0;
        [Observable]
        private double transY = 0;

        partial void OnShapeChanged(ProgressBarShape oldValue, ProgressBarShape newValue)
        {
            switch (newValue)
            {
                case ProgressBarShape.Ring:
                    RingVisibility = Visibility.Visible;
                    LineVisibility = Visibility.Collapsed;
                    BarHeight = BarWidth;
                    CurrentAngle = StartAngle + EndAngle * Progress;
                    ScaleY = flipY ? -1 : 1;
                    TransY = flipY ? BarHeight : 0;
                    return;
                case ProgressBarShape.Line:
                    LineVisibility = Visibility.Visible;
                    RingVisibility = Visibility.Collapsed;
                    BarHeight = Thickness;
                    LineLength = BarWidth * Progress;
                    ScaleY = 1;
                    TransY = 0;
                    return;
            }
        }
        partial void OnSizeChanged(double oldValue, double newValue)
        {
            if (shape == ProgressBarShape.Ring)
            {
                BarWidth = newValue;
                BarHeight = newValue;
            }
        }
        partial void OnThicknessChanged(double oldValue, double newValue)
        {
            switch (shape)
            {
                case ProgressBarShape.Line:
                    BarHeight = newValue;
                    return;
            }
        }
        partial void OnFlipXChanged(bool oldValue, bool newValue)
        {
            ScaleX = newValue ? -1 : 1;
            TransX = newValue ? BarWidth : 0;
        }
        partial void OnFlipYChanged(bool oldValue, bool newValue)
        {
            if (shape == ProgressBarShape.Ring)
            {
                ScaleY = newValue ? -1 : 1;
                TransY = newValue ? BarHeight : 0;
            }
        }
    }
}
