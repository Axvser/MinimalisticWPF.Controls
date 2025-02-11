using System.Windows;
using System.Windows.Media;

namespace MinimalisticWPF.Controls.ViewModel
{
    public partial class Button
    {
        [Observable(CanDependency:true)]
        private string _text = string.Empty;
    }
}
