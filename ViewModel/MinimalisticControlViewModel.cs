using MinimalisticWPF.Animator;
using System.Windows.Threading;

namespace MinimalisticWPF.Controls.ViewModel
{
    public partial class MinimalisticControlViewModel
    {
        public virtual double Duration { get; set; } = 0.3;
        public virtual int FrameRate { get; set; } = TransitionParams.DefaultFrameRate;
        public virtual DispatcherPriority TransitionPriority { get; set; } = DispatcherPriority.Normal;
        public virtual bool IsBeginInvoke { get; set; } = false;
        protected virtual TransitionParams ParamAction => new()
        {
            Duration = Duration,
            FrameRate = FrameRate,
            UIPriority = TransitionPriority,
            IsBeginInvoke = IsBeginInvoke
        };
    }
}
