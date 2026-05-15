using Gameplay;
using UnityEngine;

namespace Input
{
    public sealed class AIInputService : InputService
    {
        private readonly Transform[] _patrolPoints;

        private Vector3 _lookPoint;

        private int _index = 0;

        private readonly float _precision = 1f;

        public AIInputService(PlayerMoveInputConfig config, AIPatrolPointsPrefab pointsPrefab) : base(config)
        {
            _move = Vector3.forward;

            _patrolPoints = pointsPrefab.PatrolPoints;
            _lookPoint = _patrolPoints[_index].position;

            _speed = _config.Speed;
        }

        public override void ResetLevel()
        {
            base.ResetLevel();

            _index = 0;
            _lookPoint = _patrolPoints[_index].position;
        }

        public override void Update(Transform movable)
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