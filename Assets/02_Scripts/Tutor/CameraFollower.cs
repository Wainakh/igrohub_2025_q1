using UnityEngine;

namespace Igrohub
{
    public class CameraFollower : MonoBehaviour
    {
        [SerializeField] Transform _followTarget;
        
        private Vector3 _offset;

        private void Start()
        {
            _offset = transform.position - _followTarget.position;
        }

        private void Update()
        {
            if (_followTarget != default)
                transform.position = _followTarget.position + _offset;
        }
    }
}