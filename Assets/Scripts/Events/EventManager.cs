using System.Collections.Generic;

namespace Events
{
    public class EventManager
    {
        public delegate void GameEventHandler(GameEvent e);

        public GameEventHandler OnGameEvent;

        public IReadOnlyList<GameEvent> Events => _events;

        public void TriggerEvent(GameEvent e)
        {
            OnGameEvent?.Invoke(e);

            _events.Add(e);
        }

        private readonly List<GameEvent> _events = new();
    }
}