using Gameplay;
using System;

namespace UI
{
    public sealed class SceneItemsPresenter :
        IDisposable
    {
        private readonly ILoggerService _loggerService;

        private readonly IItemsSpawner _spawner;

        public SceneItemsPresenter(ILoggerService loggerService,
            IItemsSpawner spawner)
        {
            _loggerService = loggerService;
            _spawner = spawner;

            _spawner.OnSpawned += HandleSpawn;
        }

        public void Dispose()
        {
            _spawner.OnSpawned -= HandleSpawn;
        }

        private void HandleSpawn(IItem item)
        {
            _loggerService.LogSpawnItem(item.Name);
        }
    }
}