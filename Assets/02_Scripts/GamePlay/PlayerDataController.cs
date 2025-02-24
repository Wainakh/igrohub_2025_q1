public interface IPlayerDataController
{
    void AddToScore(int increaseValue);
    int GetScore();
    void SetScore(int value);
}

public class PlayerDataController : IPlayerDataController, MVC.IController
{
    private readonly IUserInterface _view;
    private readonly IPlayerData _model;

    public PlayerDataController(IUserInterface view, IPlayerData model)
    {
        _model = model;
        _view = view;
        SetScore(0);
    }

    public void SetScore(int value)
    {
        _model.Score = value;
        _view.UpdateScoreText(value);
    }

    public void AddToScore(int increaseValue) => SetScore(_model.Score + increaseValue);
    public int GetScore() => _model.Score;
}