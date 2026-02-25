using Gameplay;

namespace EventBusNamespace
{
    public sealed class EnemyKilledEvent : GameEventT<Enemy>
    {
        public EnemyKilledEvent(Enemy enemy) : base(enemy)
        {
            _name = "Enemy killed";

            _description = $"Enemy name: {enemy.Config.Name}";
        }
    }
}