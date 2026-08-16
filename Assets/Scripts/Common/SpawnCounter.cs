using Gameplay;
using System;

namespace GameTest
{
    public class SpawnCounter
    {
        public event Action<SpawnCountData> OnDataChanged;

        public SpawnCountData Data => _data;

        private readonly ISpawnService _spawnService;

        private SpawnCountData _data = new() { ActiveCount = 0, TotalCount = 0 };

        public SpawnCounter(ISpawnService spawnService)
        {
            _spawnService = spawnService;

            _spawnService.OnSpawned += HandleSpawn;
            _spawnService.OnUnspawned += HandleUnspawn;
        }

        public void Dispose()
        {
            _spawnService.OnSpawned -= HandleSpawn;
            _spawnService.OnUnspawned -= HandleUnspawn;
        }

        private void HandleSpawn()
        {
            _data.AddActiveCount(1);
            _data.AddTotalCount(1);

            OnDataChanged?.Invoke(_data);
        }

        private void HandleUnspawn()
        {
            _data.AddActiveCount(-1);

            OnDataChanged?.Invoke(_data);
        }
    }
}