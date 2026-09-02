using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class TargetsService
    {
        private readonly TargetSpawnService _spawnService;

        private readonly List<IPool<Target>> _pools = new();

        private readonly List<Target> _projectiles = new();

        public TargetsService(TargetSpawnService spawnService)
        {
            _spawnService = spawnService;
        }

        public void AddPool(IPool<Target> pool)
        {
            _pools.Add(pool);

            pool.OnNewInstantiated += HandleNewInstantiated;
        }

        private void HandleNewInstantiated(Target newInstantce)
        {
            newInstantce.OnKilled += _spawnService.Unspawn;

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
                    projectile.OnKilled -= _spawnService.Unspawn;
                }
            }
        }
    }
}