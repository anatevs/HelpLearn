using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Input
{
    public class InputHandler : MonoBehaviour
    {
        public event Action OnJupmed;

        public event Action OnShot;

        public Vector3 Move => _move;

        public Vector2 LookAngle => _look;

        public bool IsRun => _isRun;

        [SerializeField]
        private float _mouseSensitivity = 100f;

        private Vector3 _move;

        private Vector2 _look;

        private bool _isRun;

        private PlayerActions _inputActions;

        private Vector2 _inputMove;

        private void Awake()
        {
            _inputActions = new PlayerActions();
        }

        private void OnEnable()
        {
            _inputActions.Enable();

            _inputActions.Game.Jump.performed += HandleJump;

            _inputActions.Game.Shoot.performed += HandleShoot;
        }

        private void OnDisable()
        {
            _inputActions.Game.Jump.performed -= HandleJump;

            _inputActions.Game.Shoot.performed -= HandleShoot;

            _inputActions.Disable();
        }

        private void Update()
        {
            _inputMove = _inputActions.Game.Move.ReadValue<Vector2>();

            _move.x = _inputMove.x;
            _move.z = _inputMove.y;

            _move = _move.normalized;

            _look = _inputActions.Game.Look.ReadValue<Vector2>()
                * _mouseSensitivity * Time.deltaTime;

            _isRun = _inputActions.Game.Run.IsPressed();
        }

        private void HandleJump(InputAction.CallbackContext context)
        {
            OnJupmed?.Invoke();
        }

        private void HandleShoot(InputAction.CallbackContext context)
        {
            OnShot?.Invoke();
        }
    }
}