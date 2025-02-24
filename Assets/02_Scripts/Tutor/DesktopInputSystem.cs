using System;
using UnityEngine;

namespace Igrohub
{
    public class DesktopInputSystem : MonoBehaviour, IInputSystem
    {
        public event Action<Vector2> OnAxis;
        
        public void Update()
        {
            var x = Input.GetAxis("Horizontal");
            var y = Input.GetAxis("Vertical");
            if (Math.Abs(x) > float.Epsilon || Math.Abs(y) > float.Epsilon)
                OnAxis?.Invoke(new Vector2(x, y));
        }
    }
}