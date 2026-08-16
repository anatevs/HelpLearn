using System.Collections;
using UnityEngine;

namespace Gameplay
{
    public class Turret : MonoBehaviour
    {
        [SerializeField]
        private Transform _shootPoint;

        [SerializeField]
        private float _shootDelay;

        private ProjectileSpawnService _projectileService;

        private WaitForSeconds _shootWait;

        private void Awake()
        {
            _shootWait = new WaitForSeconds(_shootDelay);
        }

        public void Init(ProjectileSpawnService projectileService)
        {
            _projectileService = projectileService;

            StartCoroutine(ShootCoroutine());
        }

        private IEnumerator ShootCoroutine()
        {
            while (true)
            {
                _projectileService.Spawn(_shootPoint);

                yield return _shootWait;
            }
        }
    }
}