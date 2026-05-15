using Gameplay;
using UnityEngine;

namespace Input
{
    public abstract class InputService : IInputService
    {
        public string TypeName => _config.TypeName;
        public float Speed { get => _speed; set => _speed = value; }
        public float RotationSpeed => _config.RotationSpeed;
        public Vector3 Move => _move;
        public Vector3 LookDirection => _lookDirection;

        protected float _speed;

        protected readonly PlayerMoveInputConfig _config;

        protected Vector3 _move;

        protected Vector3 _lookDirection = Vector3.forward;

        protected InputService(PlayerMoveInputConfig config)
        {
            _config = config;
        }

        public virtual void Disable()
        {
        }

        public virtual void Dispose()
        {
        }

        public virtual void ResetLevel()
        {
            _speed = _config.Speed;
        }

        public abstract void Update(Transform movable);
    }
}