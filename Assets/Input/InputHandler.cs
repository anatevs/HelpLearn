using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Assets.Input
{
    public abstract class InputHandler : MonoBehaviour
    {
        public event Action OnJupmed;

        public event Action OnShoot;

        public event Action OnSelectableClicked;

        public event Action<GameObject> OnSelectableHovered;

        public event Action OnSelectableOut;

        public Vector3 Move => _move;

        [SerializeField]
        private LayerMask _selectablesLayer;

        protected PlayerActions _inputActions;

        protected Vector3 _castPoint;

        protected bool _isOverUI = false;

        private Vector3 _move;

        private Vector2 _inputMove;

        private Camera _camera;

        private float _castDistance = 300f;

        public abstract Vector3 GetLookDirection(Transform playerTransform);

        private void Awake()
        {
            _inputActions = new PlayerActions();

            _camera = Camera.main;
        }

        protected virtual void OnEnable()
        {
            _inputActions.Enable();

            _inputActions.Game.Jump.performed += HandleJump;
        }

        protected virtual void OnDisable()
        {
            _inputActions.Game.Jump.performed -= HandleJump;

            _inputActions.Disable();
        }

        protected virtual void Update()
        {
            _isOverUI = EventSystem.current.IsPointerOverGameObject();

            _inputMove = _inputActions.Game.Move.ReadValue<Vector2>();

            _move.x = _inputMove.x;
            _move.z = _inputMove.y;

            _move = _move.normalized;
        }

        protected bool IsScreenPointOverSelectable(Vector2 point, out RaycastHit hitInfo)
        {
            if (Physics.Raycast(_camera.ScreenPointToRay(point), out hitInfo, _castDistance))
            {
                _castPoint = hitInfo.point;

                if (((1 << hitInfo.collider.gameObject.layer) & _selectablesLayer) != 0)
                {
                    return true;
                }
            }

            return false;
        }

        private void HandleJump(InputAction.CallbackContext context)
        {
            OnJupmed?.Invoke();
        }

        protected void RaiseShoot()
        {
            OnShoot?.Invoke();
        }

        protected void RaiseSelectableClick()
        {
            OnSelectableClicked?.Invoke();
        }

        protected void RaiseSelectableHovered(GameObject go)
        {
            OnSelectableHovered?.Invoke(go);
        }

        protected void RaiseSelectableOut()
        {
            OnSelectableOut?.Invoke();
        }
    }
}