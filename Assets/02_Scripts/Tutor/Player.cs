using UnityEngine;

namespace Igrohub
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private float _speed;
        
        private IInputSystem _input;
        private Vector2? _movement;

        private void Start()
        {
            _input = FindFirstObjectByType<DesktopInputSystem>();
            _input.OnAxis += Move;
        }
        
        private void Move(Vector2 axis)
        {
            _movement = axis;
        }

        private void Update()
        {
            ApplyMovement();
        }
        
        private void ApplyMovement()
        {
            if(!_movement.HasValue)
                return;


            var direction = new Vector3(_movement.Value.x, 0, _movement.Value.y);
            var movement = Vector3.ClampMagnitude(direction, 1);
            movement *= _speed;
            movement *= Time.deltaTime;
            transform.Translate(movement);
            _movement = null;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log($"DETECT COLLIDER: {other.gameObject.name}");
        }
    }
}
