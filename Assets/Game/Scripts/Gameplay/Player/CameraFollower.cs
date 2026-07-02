using UnityEngine;

namespace Gameplay
{
    public class CameraFollower : MonoBehaviour
    {
        [SerializeField]
        private Transform _sceneObservePoint;

        private Transform _followPoint;

        private void Awake()
        {
            if (_followPoint == null)
            {
                _followPoint = _sceneObservePoint;
            }
        }

        public void Follow()
        {
            transform.SetPositionAndRotation
                (_followPoint.position, _followPoint.rotation);
        }

        public void SetPoint(Transform followPoint)
        {
            _followPoint = followPoint;
        }

        public void SetSceneObservePoint()
        {
            _followPoint = _sceneObservePoint;
        }
    }
}