using UnityEngine;

namespace Gameplay
{
    public class ShotComponent : MonoBehaviour
    {
        [SerializeField]
        private Transform _shotPoint;

        //private ProjectileSpawnService _projectileSpawn;

        //public void Init(ProjectileSpawnService projectileSpawn)
        //{
        //    _projectileSpawn = projectileSpawn;
        //}

        public void Shoot(Vector3 direction)
        {
            //_projectileSpawn.Spawn(_shotPoint.position, direction);

            ProjectileSpawnService.Instance.Spawn(_shotPoint.position, direction);
        }
    }
}