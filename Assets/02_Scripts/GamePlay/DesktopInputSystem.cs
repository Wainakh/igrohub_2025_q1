
using System;
using TMPro;
using UnityEngine;

//Инпут - начать с update -> перенести в интерфейс -> реализовать на ПК -> оставить реализацию на мобилку
public class DesktopInputSystem : ITickable, IInputSystem
{
    public event Action<Vector2> OnAxis;

    private bool _isLocked;
    
    public void Unlock()=>_isLocked = false;
    public void Lock() => _isLocked = true;

    public void Tick(float deltaTime)
    {
        if(_isLocked)
            return;
        
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");
        if (Math.Abs(x) > float.Epsilon || Math.Abs(y) > float.Epsilon)
            OnAxis?.Invoke(new Vector2(x, y));
    }
}