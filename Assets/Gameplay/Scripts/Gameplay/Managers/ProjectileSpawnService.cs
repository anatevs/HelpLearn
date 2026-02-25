using GameManagement;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class ProjectileSpawnService : DDOLClass<ProjectileSpawnService>
    {
        [SerializeField]
        private ProjectileSpawnConfig _config;

        [SerializeField]
        private Transform _poolTransform;

        [SerializeField]
        private Transform _projectilesTransform;

        private Pool<Projectile> _pool;

        private readonly HashSet<Projectile> _activeProjectiles = new();

        public void Init()
        {
            _pool = new Pool<Projectile>(_config.Prefab, _config.PoolInitCount, _poolTransform);

            Reset();
        }

        public void Reset()
        {
            if (_projectilesTransform.childCount > 0)
            {
                var activeProjectiles = _projectilesTransform.GetComponentsInChildren<Projectile>();

                foreach (var projectile in activeProjectiles)
                {
                    Unspawn(projectile);
                }
            }
        }

        public void Spawn(Vector3 position, Vector3 direction)
        {
            var projectile = _pool.Spawn(_projectilesTransform);

            projectile.transform.position = position;

            projectile.SetParams(_config.Damage, _config.Speed, direction);

            projectile.OnCollided += Unspawn;

            _activeProjectiles.Add(projectile);

            projectile.gameObject.SetActive(true);
        }

        private void Unspawn(Projectile projectile)
        {
            projectile.gameObject.SetActive(false);

            projectile.SetParams(_config.Damage, 0, Vector3.up);

            projectile.transform.position = Vector3.zero;

            _pool.Unspawn(projectile);

            projectile.OnCollided -= Unspawn;

            _activeProjectiles.Remove(projectile);
        }

        private void OnDisable()
        {
            foreach (var projectile in _activeProjectiles)
            {
                projectile.OnCollided -= Unspawn;
            }
        }
    }
}