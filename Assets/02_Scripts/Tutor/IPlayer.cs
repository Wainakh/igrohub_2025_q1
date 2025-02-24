using System;
using UnityEngine;

namespace Igrohub
{
    public interface IPlayer
    {
        Transform transform { get; }
        void SetInput(IInputSystem input);
        event Action<IPlayer, IInteractable> OnInteracted;
    }
}