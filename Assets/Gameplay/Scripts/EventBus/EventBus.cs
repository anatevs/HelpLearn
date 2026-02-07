using System;
using System.Collections.Generic;

namespace Gameplay
{
    public class EventBus
    {
        public event Action<GameEvent> OnGameEvent;

        public IReadOnlyList<GameEvent> Events => _events;

        private List<GameEvent> _events = new();

        public void RaiseEvent(GameEvent e)
        {
            OnGameEvent?.Invoke(e);

            _events.Add(e);
        }
    }
}