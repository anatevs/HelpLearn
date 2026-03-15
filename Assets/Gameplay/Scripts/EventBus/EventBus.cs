using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EventBusNamespace
{
    [CreateAssetMenu(fileName = "EventBus",
        menuName = "Configs/GameSystem/EventBus")]
    public class EventBus : ScriptableObject
    {
        public event Action<IGameEvent> OnGameEvent;
        public IReadOnlyList<IGameEvent> Events => _events;

        private readonly List<IGameEvent> _events = new();

        private readonly Dictionary<string, List<Delegate>> _listeners = new();

        public void RaiseEvent<T>(T e) where T : IGameEvent
        {
            HandleEvent(e);

            _events.Add(e);

            OnGameEvent?.Invoke(e);
        }

        public void Subscribe<T>(Action<T> action) where T : IGameEvent
        {
            var typeName = typeof(T).Name;

            if (!_listeners.ContainsKey(typeName))
            {
                _listeners.Add(typeName, new());
            }

            _listeners[typeName].Add(action);
        }

        public void Unsubscribe<T>(Action<T> action) where T : IGameEvent
        {
            var typeName = typeof(T).Name;

            if (!_listeners.ContainsKey(typeName))
            {
                return;
            }

            _listeners[typeName].Remove(action);

            if (_listeners[typeName].Count == 0)
            {
                _listeners.Remove(typeName);
            }
        }

        private void HandleEvent<T>(T e) where T : IGameEvent
        {
            if (_listeners.TryGetValue(e.EventType, out var listeners))
            {
                foreach(var listener in listeners.ToList())
                {
                    ((Action<T>)listener)(e);
                }
            }
        }
    }
}