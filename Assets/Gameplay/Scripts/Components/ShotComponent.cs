using UnityEngine;

namespace Gameplay
{
    public class ShotComponent : MonoBehaviour
    {
        [SerializeField]
        private Transform _shotPoint;

        public void Shoot(Vector3 direction)
        {
            ProjectileSpawnService.Instance.Spawn(_shotPoint.position, direction);
        }
    }
}