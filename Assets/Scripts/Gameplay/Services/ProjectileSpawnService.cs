using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public sealed class ProjectileSpawnService : MonoBehaviour
    {
        [SerializeField]
        private Transform _projectilesTransform;

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

        public void Spawn(Vector3 position, Vector3 direction, ProjectileConfig config)
        {
            var projectile = SpawnProjectile(config, _projectilesTransform);

            projectile.transform.position = position;

            projectile.SetParams(config, direction);

            projectile.OnCollided += Unspawn;

            _activeProjectiles.Add(projectile);

            projectile.gameObject.SetActive(true);
        }

        private void Unspawn(Projectile projectile)
        {
            projectile.gameObject.SetActive(false);

            projectile.SetParams(0, 0, Vector3.forward, 1, 1);

            projectile.transform.position = Vector3.zero;

            UnspawnProjectile(projectile);

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

        private Projectile SpawnProjectile(ProjectileConfig config, Transform spawnTransform)
        {
            var projectile = Instantiate(config.Prefab, spawnTransform);
            projectile.gameObject.SetActive(false);

            return projectile;
        }

        private void UnspawnProjectile(Projectile projectile)
        {
            Destroy(projectile.gameObject);
        }
    }
}