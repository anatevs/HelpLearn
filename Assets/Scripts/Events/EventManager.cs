using System.Collections.Generic;
using UnityEngine;

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

            Debug.Log(_events.Count);
        }

        private readonly List<GameEvent> _events = new();
    }
}