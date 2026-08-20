using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class ProjectileSpawnService : MonoBehaviour,
        ISpawnService
    {
        public event Action OnSpawned;
        public event Action OnUnspawned;
        public string SpawnObjectName => "Projectiles";

        public List<IInfoPool> InfoPools => _infoPools;

        public Transform PoolTransform => _poolTransform;

        [SerializeField]
        private LayerMask _damageMask;

        [SerializeField]
        private Transform _poolTransform;

        //private IPool<Projectile> _pool;

        private readonly Dictionary<ProjectileType, IPool<Projectile>> _pools = new();

        private Dictionary<ProjectileType, ProjectileTypeData> _typesData;

        private List<IInfoPool> _infoPools;

        public void InitPool(ProjectileType projectileType, IPool<Projectile> pool)
        {
            //_pool = pool;

            _pools.Add(projectileType, pool);

            if (pool is IInfoPool infoPool)
            {
                _infoPools ??= new();

                _infoPools.Add(infoPool);
            }
        }

        public void Init(ProjectileTypesConfig typeSpeedsConfig)
        {
            _typesData = typeSpeedsConfig.GetData();
        }

        public void Spawn(Transform startPoint, float speed, float lifetime, ProjectileType projectileType)
        {
            var projectile = _pools[projectileType].Get();

            projectile.transform.SetParent(transform);

            projectile.transform.SetPositionAndRotation(startPoint.position, startPoint.rotation);

            speed *= _typesData[projectileType].SpeedMultiplier;

            projectile.SetParameters(speed, lifetime, _damageMask, projectileType);

            projectile.OnDestroyed += Unspawn;

            projectile.gameObject.SetActive(true);

            OnSpawned?.Invoke();
        }

        private void Unspawn(Projectile projectile)
        {
            projectile.OnDestroyed -= Unspawn;

            projectile.gameObject.SetActive(false);

            projectile.SetParameters(0, _damageMask, 0, projectile.ProjectileType);

            projectile.transform.SetParent(_poolTransform);

            _pools[projectile.ProjectileType].Release(projectile);

            OnUnspawned?.Invoke();
        }
    }
}