using Input;
using Gameplay;
using UnityEngine;

namespace Input
{
    public sealed class TestInputService : IInputService
    {
        public string TypeName => _config.TypeName;
        public float Speed => _config.Speed;
        public float RotationSpeed => _config.RotationSpeed;
        public Vector3 Move => Vector3.forward;

        public Vector3 LookDirection => _lookDirection;

        private readonly PlayerMoveInputConfig _config;

        private Vector3 _lookDirection = Vector3.forward;

        public TestInputService(PlayerMoveInputConfig config,
            Vector3 direction)
        {
            _config = config;

            _lookDirection = direction;
        }

        public void Dispose() {}

        public void ResetLevel() {}

        public void Disable() {}

        public void Update(Transform movable)
        {
        }
    }
}