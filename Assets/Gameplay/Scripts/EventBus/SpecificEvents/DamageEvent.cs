using Gameplay;

namespace EventBusNamespace
{
    public sealed class DamageEvent : GameEventT<(HPComponent hp, int damage)>
    {
        public DamageEvent((HPComponent hp, int damage) value) : base(value)
        {
            _name = "Damage event";

            _description = $"Damage to {value.hp.gameObject.name} by {value.damage}";
        }
    }
}