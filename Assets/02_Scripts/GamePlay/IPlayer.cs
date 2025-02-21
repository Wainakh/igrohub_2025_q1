using System;
using UnityEngine;

//Персонаж - перемещение (lerp от точки к точке)
public interface IPlayer
{
    Transform transform { get; }
    void SetInput(IInputSystem input);
    event Action<IPlayer, IInteractable> OnInteracted;
}