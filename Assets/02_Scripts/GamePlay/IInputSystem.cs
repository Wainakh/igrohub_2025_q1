using System;
using UnityEngine;

namespace ReadyGamePlay
{
    public interface IInputSystem
    {
        event Action<Vector2> OnAxis;
        void Unlock();
        void Lock();
    }
}

//Инпут - начать с update -> перенести в интерфейс -> реализовать на ПК -> оставить реализацию на мобилку