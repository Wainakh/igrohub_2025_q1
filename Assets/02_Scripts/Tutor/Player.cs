using System;
using UnityEngine;

namespace Igrohub
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    public class Player : MonoBehaviour, IPlayer, ITickable
    {
        [SerializeField] private float _speed;

        private IInputSystem _input;
        private Vector2? _movement;
        Transform IPlayer.transform => base.transform;

        public event Action<IPlayer, IInteractable> OnInteracted;
        
        public void SetInput(IInputSystem input)
        {
            _input = input;
            _input.OnAxis += Move;
        }

        private void Move(Vector2 axis)
        {
            _movement = axis;
        }

        public void Tick(float deltaTime)
        {
            ApplyMovement(deltaTime);
        }

        private void ApplyMovement(float deltaTime)
        {
            if(!_movement.HasValue)
                return;

            var direction = new Vector3(_movement.Value.x, 0, _movement.Value.y);
            var movement = Vector3.ClampMagnitude(direction, 1);
            movement *= _speed;
            movement *= deltaTime;
            transform.Translate(movement);
            _movement = null;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<IInteractable>(out var interactable))
                OnInteracted?.Invoke(this, interactable);
        }

        private void OnDestroy()
        {
            _input.OnAxis -= Move;
        }
    }
}
