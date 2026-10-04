using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Input
{
    public sealed class VirtualFloatingJoystick : MonoBehaviour,
        IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        public Vector2 InputVector => _inputVector;

        public bool IsDragging => _isDragging;

        [SerializeField]
        private float _minRadius;

        [SerializeField]
        private float _maxRadius;

        private RectTransform _joystickHandle;

        private Vector2 _inputVector;

        private bool _isDragging;

        private Vector2 _startPosition;

        private float _maxRadiusSqr;
        private float _minRadiusSqr;

        private void Awake()
        {
            _maxRadiusSqr = _maxRadius * _maxRadius;
            _minRadiusSqr = _minRadius * _minRadius;

            _joystickHandle = GetComponent<RectTransform>();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnDrag(eventData);

            if (RectTransformUtility
                .RectangleContainsScreenPoint(_joystickHandle, eventData.position))
            {
                _isDragging = true;
                _startPosition = _joystickHandle.position;
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector2 delta = eventData.position - _startPosition;
            delta = ClampCached(delta);

            _joystickHandle.position = _startPosition + delta;

            if (delta.sqrMagnitude >= _minRadiusSqr)
            {
                _inputVector = delta / _maxRadius;
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            //_joystickHandle.position = _startPosition;

            _isDragging = false;
        }

        private Vector2 ClampCached(Vector2 vector)
        {
            if (vector.sqrMagnitude <= _maxRadiusSqr)
            {
                return vector;
            }

            return vector.normalized * _maxRadius;
        }
    }
}