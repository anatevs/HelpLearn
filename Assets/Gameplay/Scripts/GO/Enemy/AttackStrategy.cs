using UnityEngine;

namespace Gameplay
{
    public class AttackStrategy : EnemyStrategy
    {
        private readonly Transform _attacked;

        private readonly float _speed;

        private readonly float _period;

        private float _timer = 0f;

        public AttackStrategy(Enemy enemy,
            Transform followed) : base(enemy)
        {
            _attacked = followed;
            _speed = _enemy.Config.FollowSpeed;
            _period = _enemy.Config.ShotPeriod;
        }

        public override void ActUpdate()
        {
            _direction = (_attacked.position - _enemy.Movement.transform.position).normalized;

            _enemy.Movement.MoveUpdate(_direction, _speed);
            _enemy.Rotation.RotateUpdate(_direction, _enemy.Config.RotationSpeed);


            _timer += Time.deltaTime;
            if (_timer >= _period)
            {
                _timer = 0f;
                _enemy.Shot.Shoot(_direction);
            }
        }
    }
}