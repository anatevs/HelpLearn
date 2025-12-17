using UnityEngine;

namespace GameCore
{
    public sealed class PlayerRotation : MonoBehaviour
    {
        [SerializeField]
        private InputController _input;

        [SerializeField]
        private Transform _cameraPoint;

        private Camera _camera;

        private float[] _xRange = { -90, 90 };

        private Vector2 _currentRotation = Vector2.zero;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            _currentRotation.x = -_input.Axis.y;
            _currentRotation.x = Mathf.Clamp( _currentRotation.x, _xRange[0], _xRange[1]);

            _currentRotation.y += _input.Axis.x;

            transform.rotation = Quaternion.Euler(0, _currentRotation.y, 0);
            _cameraPoint.Rotate(Vector3.right, _currentRotation.x);
        }

        private void LateUpdate()
        {
            _camera.transform.position = _cameraPoint.position;
            _camera.transform.localRotation = _cameraPoint.rotation;
        }
    }
}