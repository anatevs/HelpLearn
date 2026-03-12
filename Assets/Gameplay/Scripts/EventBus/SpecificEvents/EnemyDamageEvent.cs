using Gameplay;

namespace EventBusNamespace
{
    public sealed class EnemyDamageEvent : GameEventT<(Enemy enemy, int damage)>
    {
        public EnemyDamageEvent((Enemy enemy, int damage) value) : base(value)
        {
            _name = "Enemy damage event";

            _description = $"Damage value {value.damage}";
        }
    }
}