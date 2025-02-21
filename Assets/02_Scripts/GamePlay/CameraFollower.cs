using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    private Transform _followtarget;
    private Vector3 _offset;

    public void SetFollowTarget(Transform target)
    {
        _followtarget = target;
        _offset = _followtarget.position - transform.position;
    }

    private void Update()
    {
        if (_followtarget != default)
            transform.position = _followtarget.position + _offset;
    }
}