using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public sealed class ProjectileSpawnService : MonoBehaviour
    {
        [SerializeField]
        private Transform _poolTransform;

        [SerializeField]
        private Transform _projectilesTransform;

        private readonly Dictionary<string, Pool<Projectile>> _pools = new();

        private readonly HashSet<Projectile> _activeProjectiles = new();

        public void ResetLevel()
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

        public void InitProjectileType(ProjectileConfig projectileConfig)
        {
            if (!_pools.ContainsKey(projectileConfig.Type))
            {
                _pools.Add(projectileConfig.Type,
                    new Pool<Projectile>(projectileConfig.Prefab, projectileConfig.PoolInitCount, _poolTransform));
            }
        }

        public void Spawn(Vector3 position, Vector3 direction, ProjectileConfig config)
        {
            var projectile = _pools[config.Type].Spawn(_projectilesTransform);

            projectile.transform.position = position;

            projectile.SetParams(config, direction);

            projectile.OnCollided += Unspawn;

            _activeProjectiles.Add(projectile);

            projectile.gameObject.SetActive(true);
        }

        private void Unspawn(Projectile projectile)
        {
            projectile.gameObject.SetActive(false);

            projectile.SetParams(projectile.Type, 0, 0, Vector3.forward, 1);

            projectile.transform.position = Vector3.zero;

            _pools[projectile.Type].Unspawn(projectile);

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