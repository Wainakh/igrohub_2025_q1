namespace Igrohub
{
    internal interface IScoreChanger : IInteractable
    {
        int AddScoreAmount { get; }
    }
}