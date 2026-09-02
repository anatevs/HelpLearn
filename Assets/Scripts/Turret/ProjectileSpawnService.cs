using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class ProjectileSpawnService :
        ISpawnService, IDisposable
    {
        public event Action OnSpawned;
        public event Action OnUnspawned;

        public List<IInfoPool> InfoPools => _infoPools;

        private readonly Dictionary<ProjectileType, ProjectileTypeData> _typesData;

        private readonly LayerMask _damagableMask;

        private readonly MovablesSystem _movablesSystem;

        private readonly LifetimedSystem _lifetimedSystem;

        private readonly Dictionary<ProjectileType, IPool<Projectile>> _pools = new();

        private List<IInfoPool> _infoPools;

        private readonly ProjectilesService _projectileService;

        public ProjectileSpawnService(ProjectileTypesConfig typesConfig,
            LayerMask damagableMask,
            MovablesSystem movablesSystem,
            LifetimedSystem lifetimedSystem)
        {
            _typesData = typesConfig.GetData();
            _damagableMask = damagableMask;
            _movablesSystem = movablesSystem;
            _lifetimedSystem = lifetimedSystem;

            _projectileService = new ProjectilesService(this);
        }

        public void Dispose()
        {
            _projectileService.Dispose();
        }

        public void AddPool(ProjectileType projectileType, IPool<Projectile> pool)
        {
            _pools.Add(projectileType, pool);

            if (pool is IInfoPool infoPool)
            {
                _infoPools ??= new();

                _infoPools.Add(infoPool);
            }

            _projectileService.AddPool(pool);
        }

        public void Spawn(Transform startPoint, float speed, float lifetime, ProjectileType projectileType)
        {
            var projectile = _pools[projectileType].Get();

            projectile.transform.SetPositionAndRotation(startPoint.position, startPoint.rotation);

            speed *= _typesData[projectileType].SpeedMultiplier;

            lifetime /= _typesData[projectileType].SpeedMultiplier;

            projectile.SetParameters(speed, lifetime, _damagableMask, projectileType);

            projectile.Activate(true);

            OnSpawned?.Invoke();

            _movablesSystem.AddMovable(projectile);
            _lifetimedSystem.AddLifetimed(projectile);
        }

        public void Unspawn(Projectile projectile)
        {
            _movablesSystem.RemoveMovable(projectile);
            _lifetimedSystem.RemoveLifetimed(projectile);

            projectile.SetParameters(0, 0, _damagableMask, projectile.ProjectileType);

            _pools[projectile.ProjectileType].Release(projectile);

            OnUnspawned?.Invoke();
        }
    }
}