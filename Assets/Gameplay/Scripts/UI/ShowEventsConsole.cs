using EventBusNamespace;
using System.Linq;
using UnityEngine;

namespace UI
{
    public sealed class ShowEventsConsole
    {
        private const string _enemiesAmountTitle = "Spawned enemies amount: ";

        public void ShowAll()
        {
            Debug.Log("");
            Debug.Log("All events:");

            foreach (var e in EventBus.Events)
            {
                ShowOneEvent(e);
            }
        }

        public void ShowLastN(int n)
        {
            Debug.Log("");
            Debug.Log($"Last {n} events:");

            var lastN = EventBus.Events
                .Reverse()
                .Take(n);

            foreach (var e in lastN)
            {
                ShowOneEvent(e);
            }
        }

        public void ShowSpawnedEnemiesAmount()
        {
            Debug.Log("");

            var spawnedCount = EventBus.Events
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