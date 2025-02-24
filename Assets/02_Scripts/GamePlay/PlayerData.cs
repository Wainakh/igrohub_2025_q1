namespace ReadyGamePlay
{
    public interface IPlayerData
    {
        int Score { get; set; }
    }

    public class PlayerData : IPlayerData, MVC.IModel
    {
        public int Score { get; set; }
    }
}