namespace Gameplay
{
    public abstract class EnemyBehaviour : IEnemyBehaviour
    {
        protected readonly Enemy _enemy;

        protected EnemyBehaviour(Enemy enemy)
        {
            _enemy = enemy;
        }

        public abstract void ActUpdate();
    }
}