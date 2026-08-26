using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class ProjectileSpawnService :
        ISpawnService
    {
        public event Action OnSpawned;
        public event Action OnUnspawned;

        public List<IInfoPool> InfoPools => _infoPools;

        private Dictionary<ProjectileType, ProjectileTypeData> _typesData;

        private readonly Transform _activesTransform;

        private readonly LayerMask _damagableMask;

        private readonly MovablesSystem _movablesSystem;

        private readonly LifetimedSystem _lifetimedSystem;

        private readonly Dictionary<ProjectileType, IPool<Projectile>> _pools = new();

        private List<IInfoPool> _infoPools;

        public ProjectileSpawnService(ProjectileTypesConfig typesConfig,
            Transform activesTransform,
            LayerMask damagableMask,
            MovablesSystem movablesSystem,
            LifetimedSystem lifetimedSystem)
        {
            _typesData = typesConfig.GetData();
            _activesTransform = activesTransform;
            _damagableMask = damagableMask;
            _movablesSystem = movablesSystem;
            _lifetimedSystem = lifetimedSystem;
        }

        public void AddPool(ProjectileType projectileType, IPool<Projectile> pool)
        {
            _pools.Add(projectileType, pool);

            if (pool is IInfoPool infoPool)
            {
                _infoPools ??= new();

                _infoPools.Add(infoPool);
            }
        }

        public void Spawn(Transform startPoint, float speed, float lifetime, ProjectileType projectileType)
        {
            var projectile = _pools[projectileType].Get();

            projectile.transform.SetParent(_activesTransform);

            projectile.transform.SetPositionAndRotation(startPoint.position, startPoint.rotation);

            speed *= _typesData[projectileType].SpeedMultiplier;

            lifetime /= _typesData[projectileType].SpeedMultiplier;

            projectile.SetParameters(speed, lifetime, _damagableMask, projectileType);

            projectile.OnDestroyed += Unspawn;

            projectile.gameObject.SetActive(true);

            OnSpawned?.Invoke();

            _movablesSystem.AddMovable(projectile);
            _lifetimedSystem.AddLifetimed(projectile);
        }

        private void Unspawn(Projectile projectile)
        {
            projectile.OnDestroyed -= Unspawn;

            projectile.gameObject.SetActive(false);

            projectile.SetParameters(0, _damagableMask, 0, projectile.ProjectileType);

            _pools[projectile.ProjectileType].Release(projectile);

            OnUnspawned?.Invoke();

            _movablesSystem.RemoveMovable(projectile);
            _lifetimedSystem.RemoveLifetimed(projectile);
        }
    }
}