using UnityEngine;

namespace Gameplay
{
    public abstract class EnemyStrategy : IEnemyStrategy
    {
        protected readonly Enemy _enemy;

        protected Vector3 _direction;

        protected EnemyStrategy(Enemy enemy)
        {
            _enemy = enemy;
        }

        public abstract void ActUpdate();
    }
}