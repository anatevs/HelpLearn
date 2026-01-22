using UnityEngine;

namespace Gameplay
{
    public class CameraFollower : MonoBehaviour
    {
        [SerializeField]
        private Transform _following;

        private Vector3 _shift;

        private void Awake()
        {
            _shift = transform.position - _following.position;
        }

        private void LateUpdate()
        {
            if (_following != null)
            {
                transform.position = _following.position + _shift;
            }
        }
    }
}