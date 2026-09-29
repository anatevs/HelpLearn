using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Input
{
    public sealed class VirtualJoystick : MonoBehaviour,
        IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        public Vector2 InputVector => _inputVector;

        public bool IsDragging => _isDragging;

        [SerializeField]
        private RectTransform _joystickBackground;

        [SerializeField]
        private RectTransform _joystickHandle;

        private Vector2 _inputVector;

        private bool _isDragging;

        private float _halfBckgrX;

        private void Awake()
        {
            _halfBckgrX = _joystickBackground.sizeDelta.x / 2;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnDrag(eventData);

            if (RectTransformUtility
                .RectangleContainsScreenPoint(_joystickBackground, eventData.position))
            {
                _isDragging = true;
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector2 position = eventData.position - (Vector2)_joystickBackground.position;
            position = Vector2.ClampMagnitude(position, _halfBckgrX);

            _joystickHandle.localPosition = position;
            _inputVector = position / (_halfBckgrX);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _inputVector = Vector2.zero;
            _joystickHandle.localPosition = Vector2.zero;

            _isDragging = false;
        }
    }
}