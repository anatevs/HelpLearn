namespace EventBusNamespace
{
    public sealed class PlayerDamageEvent : GameEventT<int>
    {
        public PlayerDamageEvent(int value) : base(value)
        {
            _name = "Player damage event";

            _description = $"Damage value: {value}";
        }
    }
}