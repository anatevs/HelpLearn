using System.Collections;
using UnityEngine;

namespace Gameplay
{
    [RequireComponent(typeof(SelectableComponent))]
    [RequireComponent(typeof(HingeJoint))]
    public class Door : MonoBehaviour
    {
        [SerializeField]
        private Color _selectedColor;

        private Color _defaultColor;

        private MeshRenderer _renderer;

        private SelectableComponent _selectable;

        private HingeJoint _hingeJoint;

        private bool _isOpened = false;

        private float _maxAngle;

        private Coroutine _closeCoroutine;

        private void Awake()
        {
            _selectable = GetComponent<SelectableComponent>();
            _hingeJoint = GetComponent<HingeJoint>();

            _maxAngle = _hingeJoint.limits.max;

            _renderer = GetComponent<MeshRenderer>();

            _defaultColor = _renderer.material.color;

            var limits = _hingeJoint.limits;
            limits.max = 0;
            _hingeJoint.limits = limits;
        }

        private void OnEnable()
        {
            _selectable.OnClicked += OpenClose;
            _selectable.OnHoverChanged += ShowSelected;
        }

        private void OnDisable()
        {
            _selectable.OnClicked -= OpenClose;
            _selectable.OnHoverChanged -= ShowSelected;
        }

        public void ResetLevel()
        {
            if (_closeCoroutine != null)
            {
                StopCoroutine(_closeCoroutine);
                _closeCoroutine = null;
            }

            SetMaxLimit(0);

            _isOpened = false;
        }

        public void ShowSelected(bool selected)
        {
            var color = selected ? _selectedColor : _defaultColor;

            _renderer.material.color = color;
        }

        private void OpenClose()
        {
            if (!_isOpened)
            {
                SetMaxLimit(_maxAngle);
            }

            var spring = _hingeJoint.spring;

            var targetAngle = _isOpened? -1f : 1f;

            targetAngle *= _hingeJoint.limits.max;

            spring.targetPosition = targetAngle;

            _hingeJoint.spring = spring;

            if (_isOpened)
            {
                _closeCoroutine = StartCoroutine(CloseDoorCoroutine());
            }

            _isOpened = !_isOpened;
        }

        private IEnumerator CloseDoorCoroutine()
        {
            while(_hingeJoint.angle > 0.01f)
            {
                yield return null;
            }

            SetMaxLimit(0);
        }

        private void SetMaxLimit(float max)
        {
            var limits = _hingeJoint.limits;
            limits.max = max;
            _hingeJoint.limits = limits;
        }
    }
}