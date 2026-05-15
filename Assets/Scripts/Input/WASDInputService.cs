using Gameplay;
using UnityEngine;

namespace Input
{
    public sealed class WASDInputService : InputService
    {
        private readonly PlayerActions _inputActions;

        private readonly Camera _camera;

        private Vector2 _inputMove;

        public WASDInputService(PlayerMoveInputConfig config) : base(config)
        {
            _speed = _config.Speed;

            _inputActions = new PlayerActions();

            _camera = Camera.main;

            _inputActions.Enable();
        }

        public override void Disable()
        {
            _inputActions.Disable();
        }

        public override void ResetLevel()
        {
            _inputActions.Enable();

            base.ResetLevel();
        }

        public override void Dispose()
        {
            _inputActions.Dispose();
        }

        public override void Update(Transform movable)
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