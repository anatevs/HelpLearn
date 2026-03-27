using System.Collections;
using UnityEngine;

namespace Gameplay
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField]
        private WeaponLook _view;

        private WeaponConfig _config;

        private ProjectileConfig _projectileConfig;

        private ProjectileSpawnService _projectileSpawn;

        private bool _isActive = false;

        private Coroutine _shotCooldown;

        private bool _canShoot = true;

        private int _remainCapacity;

        private float _remainCooldown = 0;

        public void Construct(ProjectileSpawnService spawnService)
        {
            _projectileSpawn = spawnService;
        }

        public void Init(WeaponConfig config)
        {
            _config = config;

            _remainCapacity = config.Capacity;

            _projectileConfig = config.Projectile;
        }

        public void Shoot()
        {
            if (_canShoot)
            {
                _projectileSpawn.Spawn(_view.ShotPoint.position, _view.ShotPoint.forward, _projectileConfig);

                _remainCapacity--;
                _canShoot = _remainCapacity > 0;

                if (_canShoot)
                {
                    _shotCooldown = StartCoroutine(CooldownCoroutine(_config.ShotPeriod));
                }
            }
        }

        public void ChangeActive(bool active)
        {
            _isActive = active;
            _view.gameObject.SetActive(active);
            _canShoot = _isActive || (_remainCapacity > 0);

            if (!active && _remainCooldown >= _config.ShotPeriod)
            {
                gameObject.SetActive(false);
            }
        }

        private IEnumerator CooldownCoroutine(float shotPeriod)
        {
            _canShoot = false;

            while(_remainCooldown < shotPeriod)
            {
                _remainCooldown += Time.deltaTime;

                yield return null;
            }

            _remainCooldown = 0;

            _canShoot = _isActive;

            if (!_isActive)
            {
                gameObject.SetActive(false);
            }
        }
    }
}