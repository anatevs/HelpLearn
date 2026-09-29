using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Input
{
    public sealed class InputHandlerPC : InputHandler
    {
        private bool _isOverSelectable = false;

        public override Vector3 GetLookDirection(Transform playerTransform)
        {
            var lookDirection = (_castPoint - playerTransform.position).normalized;

            lookDirection.y = 0;

            return lookDirection;
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            _inputActions.Game.Attack.performed += HandleClick;
        }

        protected override void OnDisable()
        {
            _inputActions.Game.Attack.performed -= HandleClick;

            base.OnDisable();
        }

        protected override void Update()
        {
            base.Update();

            CheckOverSelectable();
        }

        private void CheckOverSelectable()
        {
            var lookPointScreen = _inputActions.Game.LookPoint.ReadValue<Vector2>();

            if (IsScreenPointOverSelectable(lookPointScreen, out var hitInfo))
            {
                if (!_isOverSelectable)
                {
                    RaiseSelectableHovered(hitInfo.collider.gameObject);

                    _isOverSelectable = true;
                }
            }
            else if (_isOverSelectable)
            {
                RaiseSelectableOut();

                _isOverSelectable = false;
            }
        }

        private void HandleClick(InputAction.CallbackContext context)
        {
            if (_isOverUI)
            {
                return;
            }

            if (!_isOverSelectable)
            {
                RaiseShoot();
                return;
            }

            RaiseSelectableClick();
        }
    }
}