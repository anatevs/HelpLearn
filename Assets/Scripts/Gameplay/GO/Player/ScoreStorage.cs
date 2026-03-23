using System;

namespace Gameplay
{
    public class ScoreStorage : Storage,
        IDisposable
    {
        private readonly EnemySpawnService _enemySpawnService;

        public ScoreStorage(EnemySpawnService enemySpawnService)
        {
            _enemySpawnService = enemySpawnService;

            _enemySpawnService.OnEnemyKilled += ChangeValue;
        }

        void IDisposable.Dispose()
        {
            _enemySpawnService.OnEnemyKilled -= ChangeValue;
        }
    }
}