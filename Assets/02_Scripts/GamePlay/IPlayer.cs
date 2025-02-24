using System;
using UnityEngine;

namespace ReadyGamePlay
{
    public interface IPlayer
    {
        Transform transform { get; }
        void SetInput(IInputSystem input);
        event Action<IPlayer, IInteractable> OnInteracted;
    }
}

//Персонаж - перемещение (lerp от точки к точке)