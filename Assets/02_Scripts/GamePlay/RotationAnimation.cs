using System.Collections;
using UnityEngine;

public class RotationAnimation : MonoBehaviour
{
    [SerializeField] private AnimationCurve _jumpAnimation;
    [SerializeField] private float _speed = 200;
    private float step;

    private IEnumerator Start()
    {
        while (true)
        {
            transform.Rotate(Vector3.forward, _speed * Time.deltaTime);
            step += Time.deltaTime;
            if (step >= 1)
                step -= 1;
            transform.localPosition = Vector3.up * _jumpAnimation.Evaluate(step); 
            yield return null;
        }
    }
}