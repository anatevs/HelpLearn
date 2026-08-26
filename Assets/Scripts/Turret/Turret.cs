using System;
using System.Collections;
using UnityEngine;

namespace Gameplay
{
    public class Turret : MonoBehaviour
    {
        public int BarrelsAmount => _barrelData.Length;

        public LayerMask DamagableMask => _damagableMask;

        [SerializeField]
        private TurretBarrelData[] _barrelData;

        [SerializeField]
        private LayerMask _damagableMask;

        private ProjectileSpawnService _projectileService;

        private WaitForSeconds[] _shootWait;

        public void Init(ProjectileSpawnService projectileService)
        {
            _shootWait = new WaitForSeconds[_barrelData.Length];

            for (int i = 0; i < _barrelData.Length; i++)
            {
                _shootWait[i] = new WaitForSeconds(_barrelData[i].ShootPeriod);
            }

            _projectileService = projectileService;

            if (gameObject.activeSelf)
            {
                for (int i = 0; i < _barrelData.Length; i++)
                {
                    StartCoroutine(ShootCoroutine(_barrelData[i], _shootWait[i]));
                }
            }
        }

        public void ChangeBarrelParameters(float speed, float lifetime, float shootPeriod)
        {
            for (int i = 0; i < _barrelData.Length; i++)
            {
                var data = _barrelData[i];

                data.ProjectileSpeed = speed;
                data.ProjectileLifetime = lifetime;
                data.ShootPeriod = shootPeriod;

                _barrelData[i] = data;
            }
        }

        private IEnumerator ShootCoroutine(TurretBarrelData barrelData, WaitForSeconds shootWait)
        {
            yield return new WaitForSeconds(barrelData.StartDelay);

            while (true)
            {
                _projectileService.Spawn(barrelData.ShootPoint, barrelData.ProjectileSpeed, barrelData.ProjectileLifetime, barrelData.ProjectileType);

                yield return shootWait;
            }
        }
    }

    [Serializable]
    public struct TurretBarrelData
    {
        public Transform ShootPoint;

        public float ProjectileSpeed;

        public float ProjectileLifetime;

        public ProjectileType ProjectileType;

        public float StartDelay;

        public float ShootPeriod;
    }
}