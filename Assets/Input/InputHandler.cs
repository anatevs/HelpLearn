using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay
{
    public class InputHandler : MonoBehaviour
    {
        public Vector3 Position => _pointerPosition;

        public Vector3 MoveDirection => _moveDirection;

        private InputSystem_Actions _actions;

        private InputSystem_Actions.PlayerActions _playerActions;

        private Camera _camera;

        private Vector3 _pointerPosition;

        private Vector3 _moveDirection;

        private void Awake()
        {
            _actions = new();

            _playerActions = _actions.Player;

            _camera = Camera.main;
        }

        private void OnEnable()
        {
            _actions.Enable();

            _actions.Player.Move.performed += Move;
            _actions.Player.Move.canceled += StopMoving;
        }

        private void OnDisable()
        {
            _actions.Player.Move.performed -= Move;
            _actions.Player.Move.canceled -= StopMoving;

            _actions.Disable();
        }

        private void Update()
        {
            var screenPos = _playerActions.PointerPosition.ReadValue<Vector2>();

            _pointerPosition = _camera.ScreenToWorldPoint(screenPos);

            _moveDirection = _playerActions.Move.ReadValue<Vector2>();
        }

        private void Move(InputAction.CallbackContext context)
        {
            _moveDirection = context.ReadValue<Vector2>().normalized;
        }

        private void StopMoving(InputAction.CallbackContext context)
        {
            _moveDirection = Vector3.zero;
        }
    }
}