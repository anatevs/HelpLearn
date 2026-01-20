using System;

namespace Events
{
    public class GameEventT<T> : GameEvent
    {
        public T EventParam => _eventParam;

        private readonly T _eventParam;

        public GameEventT(DateTime time, string description, T eventParam) :
            base(time, description)
        {
            _eventParam = eventParam;
        }
    }
}