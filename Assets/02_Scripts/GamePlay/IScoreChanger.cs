using UnityEngine;

namespace ReadyGamePlay
{
    internal interface IScoreChanger : IInteractable
    {
        int AddScoreAmount { get; }
    }
}

//Change score when interact