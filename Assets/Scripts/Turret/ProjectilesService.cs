using System;
using System.Collections.Generic;

namespace Gameplay
{
    public class ProjectilesService : IDisposable
    {
        private readonly ProjectileSpawnService _spawnService;

        private readonly List<IPool<Projectile>> _pools = new();

        private readonly List<Projectile> _projectiles = new();

        public ProjectilesService(ProjectileSpawnService spawnService)
        {
            _spawnService = spawnService;
        }

        public void AddPool(IPool<Projectile> pool)
        {
            _pools.Add(pool);

            pool.OnNewInstantiated += HandleNewInstantiated;
        }

        private void HandleNewInstantiated(Projectile newInstantce)
        {
            newInstantce.OnDestroyed += _spawnService.Unspawn;

            _projectiles.Add(newInstantce);
        }

        public void Dispose()
        {
            foreach (var pool in _pools)
            {
                pool.OnNewInstantiated -= HandleNewInstantiated;
            }

            foreach (var projectile in _projectiles)
            {
                if (projectile != null)
                {
                    projectile.OnDestroyed -= _spawnService.Unspawn;
                }
            }
        }
    }
}