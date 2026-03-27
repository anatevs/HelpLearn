using UnityEngine;

namespace Gameplay
{
    public class CameraFollowMovement : MonoBehaviour
    {
        [SerializeField]
        protected Transform _target;

        [SerializeField]
        private Transform[] _leftRightBorders;

        [SerializeField]
        private Transform[] _bottomTopBorders;

        private Camera _mainCamera;

        private float _halfWidth;

        private float _halfHeight;

        private float _zCenterShift = 0;

        private float _zHeightShift;

        private void Awake()
        {
            _mainCamera = Camera.main;

            _halfWidth = _mainCamera.aspect * _mainCamera.orthographicSize;

            _halfHeight = _mainCamera.orthographicSize;

            if (_target.rotation.eulerAngles.x != 90)
            {
                _zCenterShift = Mathf.Abs(_target.position.y) / Mathf.Tan(_target.rotation.eulerAngles.x * Mathf.Deg2Rad);
            }

            _zHeightShift = _halfHeight / Mathf.Sin(_target.rotation.eulerAngles.x * Mathf.Deg2Rad);
        }

        protected Vector3 GetFollowingPosition()
        {
            var newPos = _target.position;
            newPos.y = transform.position.y;


            if ((_target.position.x - _halfWidth <= _leftRightBorders[0].position.x) ||
                (_target.position.x + _halfWidth >= _leftRightBorders[1].position.x))
            {
                newPos.x = transform.position.x;
            }

            var groundZCenter = _target.position.z + _zCenterShift;

            if ((groundZCenter - _zHeightShift <= _bottomTopBorders[0].position.z) ||
                (groundZCenter + _zHeightShift >= _bottomTopBorders[1].position.z))
            {
                newPos.z = transform.position.z;
            }

            return newPos;
        }
    }
}