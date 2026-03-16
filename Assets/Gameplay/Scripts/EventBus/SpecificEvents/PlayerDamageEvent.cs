namespace EventBusNamespace
{
    public sealed class PlayerDamageEvent : GameEventT<(int damage, int newHP)>
    {
        public PlayerDamageEvent((int damage, int newHP) value) : base(value)
        {
            _name = "Player damage event";

            _description = $"Damage value: {value.damage}";
        }
    }
}