using EventBusNamespace;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using GameManagement;

namespace Gameplay
{
    public class InputHandler : MonoBehaviour
    {
        public event Action OnShoot;

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

            _actions.Player.Attack.performed += Shot;

            EventBus.Subscribe<ChangeGameStateEvent>(HandleGameState);
        }

        private void OnDisable()
        {
            _actions.Player.Move.performed -= Move;
            _actions.Player.Move.canceled -= StopMoving;

            _actions.Player.Attack.performed -= Shot;

            EventBus.Unsubscribe<ChangeGameStateEvent>(HandleGameState);

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

        private void Shot(InputAction.CallbackContext context)
        {
            OnShoot?.Invoke();
        }

        private void HandleGameState(ChangeGameStateEvent e)
        {
            if (e.Value == GameStateType.Playing)
            {
                _playerActions.Enable();
                return;
            }

            _playerActions.Disable();
        }
    }
}