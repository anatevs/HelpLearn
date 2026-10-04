using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Input
{
    public class InputHandlerMobile : InputHandler
    {
        private Vector3 _lookDirection;

        private bool _isRotationFixed = false;

        private float _zeroZoneSqr = 0.001f;

        protected override void OnEnable()
        {
            base.OnEnable();

            _inputActions.Game.Attack.performed += HandleAttack;
            _inputActions.Game.Tap.performed += HandleTap;

            _inputActions.Game.LookDirection.started += HandleRotationStarted;
        }

        protected override void OnDisable()
        {
            _inputActions.Game.Attack.performed -= HandleAttack;
            _inputActions.Game.Tap.performed -= HandleTap;

            _inputActions.Game.LookDirection.started -= HandleRotationStarted;

            base.OnDisable();
        }

        public override Vector3 GetLookDirection(Transform playerTransform)
        {
            if (_isRotationFixed)
            {
                return _lookDirection;
            }

            var lookDirectionScreen = _inputActions.Game.LookDirection.ReadValue<Vector2>();

            if (lookDirectionScreen.sqrMagnitude < _zeroZoneSqr)
            {
                return _lookDirection;
            }

            _lookDirection = new Vector3(lookDirectionScreen.x, 0, lookDirectionScreen.y);

            return _lookDirection;
        }

        private void HandleAttack(InputAction.CallbackContext context)
        {
            _isRotationFixed = true;

            RaiseShoot();
        }


        private void HandleRotationStarted(InputAction.CallbackContext context)
        {
            _isRotationFixed = false;
        }

        private void HandleTap(InputAction.CallbackContext context)
        {
            var tapPosScreen = context.ReadValue<Vector2>();

            if (IsScreenPointOverSelectable(tapPosScreen, out var hitInfo))
            {
                RaiseSelectableHovered(hitInfo.collider.gameObject);

                RaiseSelectableClick();

                RaiseSelectableOut();
            }
        }
    }
}