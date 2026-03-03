using UnityEngine;

namespace Gameplay
{
    public sealed class PatrolStrategy : EnemyStrategy
    {
        private readonly Transform[] _patrolPoints;

        private readonly float _speed;

        private int _targetIndex;

        private readonly float _floatEpsilon = 0.01f;

        private Vector3 _toTargetVector;

        public PatrolStrategy(Enemy enemy,
            Transform[] patrolPoints) : base(enemy)
        {
            _type = EnemyStrategyType.Patrol;

            _patrolPoints = patrolPoints;

            if (patrolPoints == null || patrolPoints.Length == 0)
            {
                _patrolPoints = new Transform[1] { _enemy.transform };
            }

            _speed = _enemy.Config.PatrolSpeed;

            _targetIndex = 0;
            SetCurrentToTarget();
            _direction = _toTargetVector.normalized;
        }

        public override void ActUpdate()
        {
            SetCurrentToTarget();
            _direction = _toTargetVector.normalized;

            _enemy.Movement.MoveUpdate(_direction, _speed);
            _enemy.Rotation.RotateUpdate(_direction, _enemy.Config.RotationSpeed);

            if (_toTargetVector.sqrMagnitude <= _floatEpsilon)
            {
                SetNextTargetIndex();
            }
        }

        private void SetNextTargetIndex()
        {
            _targetIndex = (_targetIndex + 1) % _patrolPoints.Length;
        }

        private void SetCurrentToTarget()
        {
            _toTargetVector = _patrolPoints[_targetIndex].position - _enemy.Movement.transform.position;
        }
    }
}