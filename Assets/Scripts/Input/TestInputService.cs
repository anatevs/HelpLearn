using Gameplay;
using UnityEngine;

namespace Input
{
    public sealed class TestInputService : InputService
    {
        public TestInputService(PlayerMoveInputConfig config,
            Vector3 direction) : base(config)
        {
            _move = Vector3.forward;

            _lookDirection = direction;

            _speed = _config.Speed;
        }

        public override void Update(Transform movable)
        {
        }
    }
}