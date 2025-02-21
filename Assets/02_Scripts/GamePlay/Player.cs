//Персонаж - перемещение (lerp от точки к точке)

using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour, IPlayer
{
    [SerializeField] private float _speed;
    private IInputSystem _input;
    private Vector2? _movement;
    Transform IPlayer.transform => base.transform;
    
    public void SetInput(IInputSystem input)
    {
        input.OnAxis += Move;
    }

    public event Action<IPlayer, IInteractable> OnInteracted;

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

    private void Move(Vector2 axis)
    {
        _movement = axis;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<IInteractable>(out var interactable))
            OnInteracted?.Invoke(this, interactable);
    }
}