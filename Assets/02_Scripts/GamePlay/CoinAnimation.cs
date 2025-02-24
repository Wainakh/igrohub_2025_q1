using System;
using System.Collections;
using UnityEngine;

public class CoinAnimation : MonoBehaviour
{
    [SerializeField] private AnimationCurve _jumpAnimation;
    [SerializeField] private float _speed = 200;
    
    private float _step;

    private void OnEnable() => StartCoroutine(Animate());

    private IEnumerator Animate()
    {
        while (true)
        {
            transform.Rotate(Vector3.forward, _speed * Time.deltaTime);
            _step += Time.deltaTime;
            if (_step >= 1)
                _step -= 1;
            transform.localPosition = Vector3.up * _jumpAnimation.Evaluate(_step); 
            yield return null;
        }
    }
}