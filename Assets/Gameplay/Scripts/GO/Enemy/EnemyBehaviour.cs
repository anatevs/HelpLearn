using UnityEngine;

namespace Gameplay
{
    public abstract class EnemyBehaviour : IEnemyBehaviour
    {
        protected readonly Enemy _enemy;

        protected Vector3 _direction;

        protected EnemyBehaviour(Enemy enemy)
        {
            _enemy = enemy;
        }

        public abstract void ActUpdate();
    }
}