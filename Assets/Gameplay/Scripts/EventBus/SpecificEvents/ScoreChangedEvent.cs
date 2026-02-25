namespace EventBusNamespace
{
    public sealed class ScoreChangedEvent : GameEventT<int>
    {
        public ScoreChangedEvent(int value) : base(value)
        {
            _name = "Score changed";
            _description = $"Changed by value: {value}";
        }
    }
}