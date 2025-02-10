using MinimalisticWPF.Theme;
using MinimalisticWPF.TransitionSystem;
using System.Windows.Media;

namespace MinimalisticWPF.Controls.ViewModel
{
    public partial class TextBox
    {
        [Constructor]
        private void SetInitialTheme()
        {
            CurrentTheme = typeof(Dark);
        }

        [Observable(CanDependency: true)]
        [Dark("Cyan")]
        [Light("Lime")]
        private Brush caretBrush = Brushes.Cyan;

        [Observable(CanDependency: true)]
        [Dark("White")]
        [Light("Black")]
        private Brush foreground = Brushes.White;

        [Observable(CanDependency:true)]
        [Dark("#32FFFFFF")]
        [Light("#32000000")]
        private Brush background = RGB.FromString("#32FFFFFF").Brush;

        [Observable(CanDependency: true)]
        [Dark("White")]
        [Light("Black")]
        private Brush borderBrush = Brushes.White;

        [Observable]
        private double visualLength = 0;
    }
}
