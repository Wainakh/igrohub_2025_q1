using System;
using UnityEngine;

//Инпут - начать с update -> перенести в интерфейс -> реализовать на ПК -> оставить реализацию на мобилку
public interface IInputSystem
{
    event Action<Vector2> OnAxis;
}