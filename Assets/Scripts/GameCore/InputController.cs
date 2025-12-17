using UnityEngine;

namespace GameCore
{
    public sealed class InputController : MonoBehaviour
    {
        public Vector2 Axis => _axis;
        public Vector2 SceenPos { get; private set; } = Vector2.zero;

        [SerializeField]
        private float _mouseSensitivity = 10f;

        private Vector2 _axis = Vector2.zero;

        private void Update()
        {
            var sensitivity = _mouseSensitivity * Time.deltaTime;
            _axis.x = Input.GetAxis("Mouse X") * sensitivity;
            _axis.y = Input.GetAxis("Mouse Y") * sensitivity;

            SceenPos = Input.mousePosition;
        }
    }
}