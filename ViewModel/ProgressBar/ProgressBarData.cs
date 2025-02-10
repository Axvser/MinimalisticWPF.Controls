using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MinimalisticWPF.Controls.ViewModel
{
    public partial class ProgressBar
    {
        [Observable(CanDependency: true)]
        private double _progress = (double)0;

        [Observable(CanDependency: true)]
        private double startAngle = (double)0;

        [Observable(CanDependency: true)]
        private double endAngle = (double)360;

        [Observable]
        private double maxAngle = 360;

        [Observable]
        private double currentAngle = 0;

        [Observable]
        private double lineLength = 0;

        partial void OnProgressChanged(double oldValue, double newValue)
        {
            switch (shape)
            {
                case ProgressBarShape.Ring:
                    CurrentAngle = StartAngle + EndAngle * Progress;
                    return;
                case ProgressBarShape.Line:
                    LineLength = BarWidth * Progress;
                    return;
            }
        }
        partial void OnStartAngleChanged(double oldValue, double newValue)
        {
            MaxAngle = startAngle + endAngle;
        }
        partial void OnEndAngleChanged(double oldValue, double newValue)
        {
            MaxAngle = startAngle + endAngle;
        }
        partial void OnMaxAngleChanged(double oldValue, double newValue)
        {
            CurrentAngle = StartAngle + EndAngle * Progress;
        }
    }
}
