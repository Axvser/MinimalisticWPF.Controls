using System.Windows.Controls;
using System.Windows.Media;

namespace MinimalisticWPF.Controls
{
    public enum ProgressBarShape
    {
        Ring,
        Line
    }

    [DataContextConfig(nameof(ProgressBar), "MinimalisticWPF.Controls.ViewModel")]
    public partial class ProgressBar : UserControl
    {
        public ProgressBar()
        {
            InitializeComponent();
        }
    }
}
