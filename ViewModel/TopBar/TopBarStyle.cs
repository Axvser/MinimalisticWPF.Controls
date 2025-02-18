using MinimalisticWPF.Theme;
using MinimalisticWPF.TransitionSystem;
using System.Windows;
using System.Windows.Media;

namespace MinimalisticWPF.Controls.ViewModel
{
    public partial class TopBar
    {
        [Observable(CanDependency: true)]
        private string title = "Application";

        [Observable(CanDependency: true, CanIsolated: true)]
        [Dark("White")]
        [Light("Black")]
        private Brush titleBrush = Brushes.White;

        [Observable(CanDependency: true, CanIsolated: true)]
        [Dark(nameof(Brushes.Transparent))]
        [Light(nameof(Brushes.Transparent))]
        private Brush background = Brushes.Transparent;

        [Observable]
        private Thickness controlMargin = new Thickness(3, 0, 3, 0);
    }
}
