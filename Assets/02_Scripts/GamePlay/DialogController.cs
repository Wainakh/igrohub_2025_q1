using System;

public class DialogController
{
    private DialogConfig _config;
    private DialogView _view;
    private Action _onFinish;
    private int _speechId;

    public DialogController(DialogConfig config, DialogView view, Action onFinish)
    {
        _config = config;
        _view = view;
        _onFinish = onFinish;
    }

    public void Interrupt() => TurnOfView();
    public void Start() => ShowById(_speechId = 0);
    private void ShowById(int id) => _view.Show(_config.GetSpeeches()[id], GoNext);

    private void GoNext()
    {
        _speechId++;
        if (_speechId < _config.GetSpeeches().Count)
            ShowById(_speechId);
        else
            FinishDialog();
    }

    private void FinishDialog()
    {
        TurnOfView();
        _onFinish?.Invoke();
    }
    
    private void TurnOfView()
    {
        _view.HideAll();
    }
}