using MinimalisticWPF.Theme;
using MinimalisticWPF.TransitionSystem;
using System.Windows;
using System.Windows.Media;

namespace MinimalisticWPF.Controls.ViewModel
{
    public partial class ProgressBar
    {
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
        partial void OnShapeChanged(ProgressBarShape oldValue, ProgressBarShape newValue)
        {
            switch (newValue)
            {
                case ProgressBarShape.Ring:
                    RingVisibility = Visibility.Visible;
                    LineVisibility = Visibility.Collapsed;
                    BarHeight = BarWidth;
                    CurrentAngle = StartAngle + EndAngle * Progress;
                    return;
                case ProgressBarShape.Line:
                    LineVisibility = Visibility.Visible;
                    RingVisibility = Visibility.Collapsed;
                    BarHeight = Thickness;
                    LineLength = BarWidth * Progress;
                    return;
            }
        }

        [Observable(CanDependency: true)]
        private double size = (double)200;
        partial void OnSizeChanged(double oldValue, double newValue)
        {
            if (shape == ProgressBarShape.Ring)
            {
                BarWidth = newValue;
                BarHeight = newValue;
            }
        }

        [Observable(CanDependency: true)]
        private double _thickness = (double)3;
        partial void OnThicknessChanged(double oldValue, double newValue)
        {
            switch (shape)
            {
                case ProgressBarShape.Line:
                    BarHeight = newValue;
                    return;
            }
        }

        [Observable(CanDependency: true)]
        private Brush foreFill = Brushes.Cyan;
        [Observable(CanDependency: true)]
        private Brush backFill = Brushes.Gray;
    }
}
