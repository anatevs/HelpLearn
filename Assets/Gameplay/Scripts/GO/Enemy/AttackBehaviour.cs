using UnityEngine;

namespace Gameplay
{
    public class AttackBehaviour : EnemyBehaviour
    {
        private readonly Transform _attacked;

        private readonly float _speed;

        private Vector3 _direction;

        public AttackBehaviour(Enemy enemy,
            Transform followed) : base(enemy)
        {
            _attacked = followed;
            _speed = _enemy.Config.FollowSpeed;
        }

        public override void ActUpdate()
        {
            _direction = (_attacked.position - _enemy.Movement.transform.position).normalized;

            _enemy.Movement.MoveUpdate(_direction, _speed);
        }
    }
}