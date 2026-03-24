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

        public void Shoot(Vector3 direction)
        {
            _spawnService.Spawn(_shotPoint.position, direction);
        }
    }
}