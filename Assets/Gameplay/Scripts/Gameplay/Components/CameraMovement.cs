using UnityEngine;

namespace Gameplay
{
    public sealed class CameraMovement : MonoBehaviour
    {
        [SerializeField]
        private GameObject _target;

        [SerializeField]
        private Transform[] _leftRightBorder = new Transform[2];

        [SerializeField]
        private Transform[] _bottomTopBorder = new Transform[2];

        private Camera _mainCamera;

        private float _halfWidth;

        private float _halfHeight;

        private void Awake()
        {
            _mainCamera = Camera.main;

            _halfWidth = _mainCamera.aspect * _mainCamera.orthographicSize;

            _halfHeight = _mainCamera.orthographicSize;
        }

        private void LateUpdate()
        {
            var newPos = _target.transform.position;
            newPos.z = transform.position.z;

            if ((_target.transform.position.x - _halfWidth <= _leftRightBorder[0].position.x) ||
                (_target.transform.position.x + _halfWidth >= _leftRightBorder[1].position.x))
            {
                newPos.x = transform.position.x;
            }

            if ((_target.transform.position.y - _halfHeight <= _bottomTopBorder[0].position.y) ||
                (_target.transform.position.y + _halfHeight >= _bottomTopBorder[1].position.y))
            {
                newPos.y = transform.position.y;
            }

            transform.position = newPos;
        }
    }
}