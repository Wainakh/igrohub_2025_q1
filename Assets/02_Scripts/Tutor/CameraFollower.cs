﻿using UnityEngine;

namespace Igrohub
{
    public class CameraFollower : MonoBehaviour
    {
        Transform _followTarget;
        private Vector3 _offset;

        public void SetFollowTarget(Transform target)
        {
            _followTarget = target;
            _offset = transform.position - _followTarget.position;
        }
        
        private void Update()
        {
            if (_followTarget != default)
                transform.position = _followTarget.position + _offset;
        }
    }
}