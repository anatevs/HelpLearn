using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public sealed class ProjectileSpawnService : MonoBehaviour
    {
        [SerializeField]
        private ProjectileConfig _config;

        [SerializeField]
        private Transform _poolTransform;

        [SerializeField]
        private Transform _projectilesTransform;

        private Pool<Projectile> _pool;

        private readonly HashSet<Projectile> _activeProjectiles = new();

        public void Construct()
        {
            _pool = new Pool<Projectile>(_config.Prefab, _config.PoolInitCount, _poolTransform);
        }

        public void Init()
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

        public void Spawn(Vector3 position, Vector3 direction, ProjectileConfig config)
        {
            var projectile = _pool.Spawn(_projectilesTransform);

            projectile.transform.position = position;

            projectile.SetParams(config, direction);

            projectile.OnCollided += Unspawn;

            _activeProjectiles.Add(projectile);

            projectile.gameObject.SetActive(true);
        }

        private void Unspawn(Projectile projectile)
        {
            projectile.gameObject.SetActive(false);

            projectile.SetParams(0, 0, Vector3.forward, 1);

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