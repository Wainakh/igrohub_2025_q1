using System;
using ReadyGamePlay;

namespace Igrohub
{
    public interface IDialogManager
    {
        void InterruptDialog();
        void StartDialog(int configId, Action onFinish);
        void StartDialog(DialogConfig config, Action onFinish);
    }
}