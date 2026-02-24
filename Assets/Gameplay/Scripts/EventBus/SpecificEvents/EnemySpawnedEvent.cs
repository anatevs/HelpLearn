using Gameplay;

namespace EventBusNamespace
{
    public class EnemySpawnedEvent : GameEventT<Enemy>
    {
        public EnemySpawnedEvent(Enemy enemy) : base(enemy)
        {
            _name = "Enemy spawned";

            _description = $"Enemy name: {enemy.Config.Name}, start strategy: {enemy.StrategyName}";
        }
    }
}