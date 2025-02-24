using System;
using UnityEngine;

namespace Igrohub
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float _speed;

        private void Update()
        {
            var x = Input.GetAxis("Horizontal");
            var y = Input.GetAxis("Vertical");
            if (Math.Abs(x) > float.Epsilon || Math.Abs(y) > float.Epsilon)
            {
                var direction = new Vector3(x, 0, y);
                var movement = Vector3.ClampMagnitude(direction, 1);
                movement *= _speed;
                movement *= Time.deltaTime;
                transform.Translate(movement);
            }
        }
    }
}
