using System;
using System.Collections.Generic;
using System.Linq;

namespace EventBusNamespace
{
    public static class EventBus
    {
        public static IReadOnlyList<IGameEvent> Events => _events;

        private static readonly List<IGameEvent> _events = new();

        private static readonly Dictionary<string, List<Delegate>> _listeners = new();

        public static void RaiseEvent<T>(T e) where T : IGameEvent
        {
            HandleEvent(e);

            _events.Add(e);
        }

        public static void Subscribe<T>(Action<T> action) where T : IGameEvent
        {
            var typeName = typeof(T).Name;

            if (!_listeners.ContainsKey(typeName))
            {
                _listeners.Add(typeName, new());
            }

            _listeners[typeName].Add(action);
        }

        public static void Unsubscribe<T>(Action<T> action) where T : IGameEvent
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

        private static void HandleEvent<T>(T e) where T : IGameEvent
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