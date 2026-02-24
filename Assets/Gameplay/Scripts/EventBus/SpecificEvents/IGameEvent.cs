namespace EventBusNamespace
{
    public interface IGameEvent
    {
        public string Name { get; }

        public string Description { get; }

        public string EventType { get; }
    }
}