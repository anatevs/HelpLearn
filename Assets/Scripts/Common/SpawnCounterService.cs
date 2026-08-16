using Gameplay;
using System;

namespace GameTest
{
    public class SpawnCounterService
    {
        public event Action<SpawnCounter, string> OnServiceAdded;

        public void AddSpawnService(ISpawnService spawnService)
        {
            var counter = new SpawnCounter(spawnService);

            OnServiceAdded?.Invoke(counter, spawnService.SpawnObjectName);
        }
    }
}