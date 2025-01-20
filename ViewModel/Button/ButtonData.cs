using System.Windows;
using System.Windows.Media;

namespace MinimalisticWPF.Controls.ViewModel
{
    public partial class Button
    {
        [Observable]
        private string _text = string.Empty;
    }
}
