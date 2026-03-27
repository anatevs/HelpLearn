using UnityEngine;

namespace Gameplay
{
    public sealed class ShotComponent : MonoBehaviour
    {
        [SerializeField]
        private Transform _shotPoint;

        private ProjectileSpawnService _spawnService;

        public void Construct(ProjectileSpawnService spawnService)
        {
            _spawnService = spawnService;
        }

        public void Shoot(WeaponConfig weaponConfig)
        {
            _spawnService.Spawn(_shotPoint.position, _shotPoint.forward, weaponConfig.Projectile);
        }
    }
}