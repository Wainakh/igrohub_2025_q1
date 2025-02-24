public interface IPlayerDataController 
{
    void AddToScore(int increaseValue);
}

public class PlayerDataController : IPlayerDataController, MVC.IController
{
    private readonly IUserInterface _view;
    private readonly IPlayerData _model;
    public PlayerDataController(IUserInterface view, IPlayerData model)
    {
        _model = model;
        _view = view;
        SetNewScore(0);
    }

    public void SetNewScore(int value)
    {
        _model.Score = value;
        _view.UpdateScoreText(value);
    }

    public void AddToScore(int increaseValue)
    {
        SetNewScore(_model.Score + increaseValue);
    }
}