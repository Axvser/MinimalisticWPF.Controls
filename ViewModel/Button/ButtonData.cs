using MinimalisticWPF.Animator;
using System.Windows;
using System.Windows.Media;

namespace MinimalisticWPF.Controls.ViewModel
{
    public partial class ButtonViewModel
    {
        [Observable]
        private string _text = string.Empty;
    }
}
