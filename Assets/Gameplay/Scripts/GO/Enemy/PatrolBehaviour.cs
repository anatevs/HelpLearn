using UnityEngine;

namespace Gameplay
{
    public class PatrolBehaviour : EnemyBehaviour
    {
        private readonly Transform[] _patrolPoints;

        private readonly float _speed;

        private int _targetIndex;

        private readonly float _sqrEpsilon = 0.01f;

        public PatrolBehaviour(Enemy enemy,
            Transform[] patrolPoints) : base(enemy)
        {
            _patrolPoints = patrolPoints;

            if (patrolPoints == null || patrolPoints.Length == 0)
            {
                _patrolPoints = new Transform[1] { _enemy.transform };
            }

            _speed = _enemy.Config.PatrolSpeed;

            _targetIndex = 0;
            _direction = GetCurrentDirection().normalized;
        }

        public override void ActUpdate()
        {
            _enemy.Movement.MoveUpdate(_direction, _speed);
            _enemy.Rotation.RotateUpdate(_direction, _enemy.Config.RotationSpeed);

            if (GetCurrentDirection().sqrMagnitude <= _sqrEpsilon)
            {
                SetNextTargetIndex();
                _direction = GetCurrentDirection().normalized;
            }
        }

        private void SetNextTargetIndex()
        {
            _targetIndex = (_targetIndex + 1) % _patrolPoints.Length;
        }

        private Vector3 GetCurrentDirection()
        {
            return _patrolPoints[_targetIndex].position - _enemy.Movement.transform.position;
        }
    }
}