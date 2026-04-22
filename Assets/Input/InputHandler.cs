using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Assets.Input
{
    public sealed class InputHandler : MonoBehaviour
    {
        public event Action OnJupmed;

        public event Action OnShoot;

        public event Action OnSelectableClicked;

        public event Action<GameObject> OnSelectableHovered;

        public event Action OnSelectableOut;

        public Vector3 Move => _move;

        public Vector3 LookPoint => _lookPoint;

        [SerializeField]
        private LayerMask _selectablesLayer;

        private Vector3 _move;

        private Vector3 _lookPoint;

        private PlayerActions _inputActions;

        private Vector2 _inputMove;

        private Camera _camera;

        private bool _isOverUI = false;

        private bool _isOverSelectable = false;

        private float _castDistance = 300f;

        private void Awake()
        {
            _inputActions = new PlayerActions();

            _camera = Camera.main;
        }

        private void OnEnable()
        {
            _inputActions.Enable();

            _inputActions.Game.Jump.performed += HandleJump;
            _inputActions.Game.Attack.performed += HandleClick;
        }

        private void OnDisable()
        {
            _inputActions.Game.Jump.performed -= HandleJump;
            _inputActions.Game.Attack.performed -= HandleClick;

            _inputActions.Disable();
        }

        private void Update()
        {
            _isOverUI = EventSystem.current.IsPointerOverGameObject();

            if (!_isOverUI)
            {
                _inputMove = _inputActions.Game.Move.ReadValue<Vector2>();

                _move.x = _inputMove.x;
                _move.z = _inputMove.y;

                _move = _move.normalized;

                var lookPointScreen = _inputActions.Game.Look.ReadValue<Vector2>();

                if (Physics.Raycast(_camera.ScreenPointToRay(lookPointScreen), out var hitInfo, _castDistance))
                {
                    _lookPoint = hitInfo.point;

                    if (((1<< hitInfo.collider.gameObject.layer) & _selectablesLayer) != 0)
                    {
                        if (!_isOverSelectable)
                        {
                            OnSelectableHovered?.Invoke(hitInfo.collider.gameObject);
                        }

                        _isOverSelectable = true;
                    }
                    else
                    {
                        if (_isOverSelectable)
                        {
                            OnSelectableOut?.Invoke();
                        }

                        _isOverSelectable = false;
                    }
                }
            }
        }

        private void HandleJump(InputAction.CallbackContext context)
        {
            if (!_isOverUI)
            {
                OnJupmed?.Invoke();
            }
        }

        private void HandleClick(InputAction.CallbackContext context)
        {
            if (!_isOverUI && !_isOverSelectable)
            {
                OnShoot?.Invoke();
            }

            if (_isOverSelectable && !_isOverUI)
            {
                OnSelectableClicked?.Invoke();
            }
        }
    }
}