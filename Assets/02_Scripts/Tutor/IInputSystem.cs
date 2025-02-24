using System;
using UnityEngine;

namespace Igrohub
{
    public interface IInputSystem
    {
        event Action<Vector2> OnAxis;
    }
}