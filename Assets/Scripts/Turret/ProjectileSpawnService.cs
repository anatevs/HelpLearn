using System;
using UnityEngine;

namespace Gameplay
{
    public class ProjectileSpawnService : MonoBehaviour,
        ISpawnService
    {
        public event Action OnSpawned;
        public event Action OnUnspawned;
        public string SpawnObjectName => "Projectiles";

        [SerializeField]
        private float _speed;

        [SerializeField]
        private LayerMask _damageMask;

        [SerializeField]
        private Transform _poolTransform;

        private IPool<Projectile> _pool;

        public void Init(IPool<Projectile> pool)
        {
            _pool = pool;
        }

        public void Spawn(Transform startPoint)
        {
            var projectile = _pool.Get();

            projectile.transform.SetParent(transform);
            projectile.transform.SetPositionAndRotation(startPoint.position, startPoint.rotation);

            projectile.SetParameters(_speed, _damageMask);

            projectile.OnCollided += Unspawn;

            projectile.gameObject.SetActive(true);

            OnSpawned?.Invoke();
        }

        private void Unspawn(Projectile projectile)
        {
            projectile.gameObject.SetActive(false);

            projectile.SetParameters(0, _damageMask);

            projectile.transform.SetParent(_poolTransform);

            _pool.Release(projectile);

            OnUnspawned?.Invoke();
        }
    }
}