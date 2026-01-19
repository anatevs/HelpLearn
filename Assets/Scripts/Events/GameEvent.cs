namespace Events
{
    public class GameEvent
    {
        public EventType Type => _type;
        public float Time => _time;

        public string Description => _description;

        protected EventType _type;

        protected readonly float _time;

        protected readonly string _description;

        public GameEvent(float time, string description)
        {
            _time = time;
            _description = description;
        }

        public GameEvent(EventType type, float time, string description)
        {
            _type = type;
            _time = time;
            _description = description;
        }
    }
}