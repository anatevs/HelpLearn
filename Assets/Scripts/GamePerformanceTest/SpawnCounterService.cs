using Gameplay;
using System;
using System.Collections.Generic;

namespace GameTest
{
    public class SpawnCounterService : IDisposable
    {
        public event Action<SpawnCounter, string, List<IInfoPool>> OnServiceAdded;

        public event Action<int> OnPeakCountRaised;

        private readonly List<SpawnCounter> _spawnCounters = new();

        private int _objectsPeakCount = 0;

        public void AddSpawnService(ISpawnService spawnService)
        {
            var counter = new SpawnCounter(spawnService);

            OnServiceAdded?.Invoke(counter, spawnService.SpawnObjectName, spawnService.InfoPools);

            _spawnCounters.Add(counter);

            counter.OnPeakRaised += HandlePickCountRaise;
        }

        public void Dispose()
        {
            foreach (var counter in _spawnCounters)
            {
                counter.OnPeakRaised -= HandlePickCountRaise;
            }
        }

        private void HandlePickCountRaise(int changeValue)
        {
            _objectsPeakCount += changeValue;

            OnPeakCountRaised?.Invoke(_objectsPeakCount);
        }
    }
}