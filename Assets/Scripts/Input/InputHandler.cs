using Gameplay;
using UnityEngine;

namespace Assets.Input
{
    public sealed class InputHandler : IInputService
    {
        public PlayerMoveInputConfig Config => _config;
        public float Speed => _config.Speed;
        public float RotationSpeed => _config.RotationSpeed;
        public Vector3 Move => _move;
        public Vector3 LookDirection => _lookDirection;

        private readonly PlayerActions _inputActions;

        private readonly Camera _camera;

        private readonly PlayerMoveInputConfig _config;

        private Vector3 _move;

        private Vector3 _lookDirection = Vector3.forward;

        private Vector2 _inputMove;

        public InputHandler(PlayerMoveInputConfig config)
        {
            _config = config;

            _inputActions = new PlayerActions();

            _camera = Camera.main;

            _inputActions.Enable();
        }

        public void Dispose()
        {
            _inputActions.Dispose();
        }

        public void ResetLevel()
        {
            _inputActions.Enable();
        }

        public void Disable()
        {
            _inputActions.Disable();
        }

        public void Update(Transform movable)
        {
            _inputMove = _inputActions.Game.Move.ReadValue<Vector2>();

            _move.x = _inputMove.x;
            _move.z = _inputMove.y;

            _move = _move.normalized;

            var lookPointScreen = _inputActions.Game.Look.ReadValue<Vector2>();

            _lookDirection = new Vector3(
                lookPointScreen.x,
                0,
                _camera.nearClipPlane);

            _lookDirection = movable.TransformDirection(_lookDirection).normalized;
        }
    }
}