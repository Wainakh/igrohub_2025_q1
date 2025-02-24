namespace Igrohub
{
    public interface IPlayerDataController
    {
        void AddToScore(int increaseValue);
        int GetScore();
        void SetScore(int value);
    }
}