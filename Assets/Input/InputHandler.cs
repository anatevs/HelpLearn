using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Input
{
    public sealed class InputHandler : MonoBehaviour
    {
        public event Action OnJupmed;

        public event Action OnShoot;

        public Vector3 Move => _move;

        public Vector3 LookPoint => _lookPoint;

        private Vector3 _move;

        private Vector3 _lookPoint;

        private PlayerActions _inputActions;

        private Vector2 _inputMove;

        private Camera _camera;

        private void Awake()
        {
            _inputActions = new PlayerActions();

            _camera = Camera.main;
        }

        private void OnEnable()
        {
            _inputActions.Enable();

            _inputActions.Game.Jump.performed += HandleJump;
            _inputActions.Game.Attack.performed += HandleShoot;
        }

        private void OnDisable()
        {
            _inputActions.Game.Jump.performed -= HandleJump;
            _inputActions.Game.Attack.performed -= HandleShoot;

            _inputActions.Disable();
        }

        private void Update()
        {
            _inputMove = _inputActions.Game.Move.ReadValue<Vector2>();

            _move.x = _inputMove.x;
            _move.z = _inputMove.y;

            _move = _move.normalized;

            var lookPointScreen = _inputActions.Game.Look.ReadValue<Vector2>();

            if (Physics.Raycast(_camera.ScreenPointToRay(lookPointScreen), out var hitInfo))
            {
                _lookPoint = hitInfo.point;
            }
        }

        private void HandleJump(InputAction.CallbackContext context)
        {
            OnJupmed?.Invoke();
        }

        private void HandleShoot(InputAction.CallbackContext context)
        {
            OnShoot?.Invoke();
        }
    }
}