//Диалоги - DialogManager, верстка префаба диалогового окна, Scriptable object. ЗАгрузка конфигов диалогов из ресурсов. Диалог стартер

using System;

public interface IDialogManager
{
    void InterruptDialog();
    void StartDialog(int configId, Action onFinish);
    void StartDialog(DialogConfig config, Action onFinish);
}