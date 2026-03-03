using EventBusNamespace;
using UnityEngine;

namespace Gameplay
{
    public sealed class ShotComponent : MonoBehaviour
    {
        [SerializeField]
        private Transform _shotPoint;

        public void Shoot(Vector3 direction)
        {
            ProjectileSpawnService.Instance.Spawn(_shotPoint.position, direction);

            EventBus.RaiseEvent(new ShotEvent(this));
        }
    }
}