using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Input
{
    public class InputHandlerMobile : InputHandler
    {
        protected override void OnEnable()
        {
            base.OnEnable();

            _inputActions.Game.Attack.performed += HandleAttack;
            _inputActions.Game.Tap.performed += HandleTap;
        }

        protected override void OnDisable()
        {
            _inputActions.Game.Attack.performed -= HandleAttack;
            _inputActions.Game.Tap.performed -= HandleTap;

            base.OnDisable();
        }

        public override Vector3 GetLookDirection(Transform playerTransform)
        {
            var lookDirectionScreen = _inputActions.Game.LookDirection.ReadValue<Vector2>();

            return new Vector3(lookDirectionScreen.x, 0, lookDirectionScreen.y);
        }

        private void HandleAttack(InputAction.CallbackContext context)
        {
            RaiseShoot();
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