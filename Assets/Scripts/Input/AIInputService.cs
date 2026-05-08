using Gameplay;
using UnityEngine;

namespace Input
{
    public sealed class AIInputService : IInputService
    {
        public string TypeName => _config.TypeName;
        public float Speed => _config.Speed;
        public float RotationSpeed => _config.RotationSpeed;
        public Vector3 Move => Vector3.forward;

        public Vector3 LookDirection => _lookDirection;

        private readonly PlayerMoveInputConfig _config;

        private readonly Transform[] _patrolPoints;

        private Vector3 _lookPoint;
        private Vector3 _lookDirection = Vector3.forward;

        private int _index = 0;

        private readonly float _precision = 1f;

        public AIInputService(PlayerMoveInputConfig config,
            Transform[] patrolPoints)
        {
            _config = config;
            _patrolPoints = patrolPoints;
            _lookPoint = _patrolPoints[_index].position;
        }

        public void Dispose() {}

        public void ResetLevel()
        {
            _index = 0;
            _lookPoint = _patrolPoints[_index].position;
        }

        public void Disable() {}

        public void Update(Transform movable)
        {
            if ((_lookPoint - movable.position).sqrMagnitude <= _precision)
            {
                _index = (_index + 1) % _patrolPoints.Length;
                _lookPoint = _patrolPoints[_index].position;
            }

            _lookDirection = (_lookPoint - movable.position).normalized;
        }
    }
}