using System;

namespace Events
{
    public class GameEvent
    {
        public EventType Type => _type;
        public DateTime Time => _time;
        public string Description => _description;

        protected EventType _type;

        protected readonly DateTime _time;

        protected readonly string _description;

        public GameEvent(DateTime time, string description)
        {
            _time = time;
            _description = description;
        }

        public GameEvent(EventType type, DateTime time, string description)
        {
            _type = type;
            _time = time;
            _description = description;
        }
    }
}