using EventBusNamespace;
using System.Linq;
using UnityEngine;

namespace UI
{
    public sealed class ShowEventsConsole
    {
        private EventBus _eventBus;

        private const string _enemiesAmountTitle = "Spawned enemies amount: ";
        private const string _delimiter = "**************";

        public ShowEventsConsole(EventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public void ShowAll()
        {
            Debug.Log(_delimiter);
            Debug.Log("All events:");

            foreach (var e in _eventBus.Events)
            {
                ShowOneEvent(e);
            }
        }

        public void ShowLastN(int n)
        {
            Debug.Log(_delimiter);
            Debug.Log($"Last {n} events:");

            var lastN = _eventBus.Events
                .Reverse()
                .Take(n);

            foreach (var e in lastN)
            {
                ShowOneEvent(e);
            }
        }

        public void ShowSpawnedEnemiesAmount()
        {
            Debug.Log(_delimiter);

            var spawnedCount = _eventBus.Events
                .Where((e) => e.EventType == typeof(EnemySpawnedEvent).Name)
                .Count();

            Debug.Log($"{_enemiesAmountTitle}{spawnedCount}");
        }

        private void ShowOneEvent(IGameEvent e)
        {
            Debug.Log(EventUIInfo.GetEventString(e));
        }
    }
}