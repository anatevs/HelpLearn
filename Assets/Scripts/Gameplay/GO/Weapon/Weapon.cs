using System;
using System.Collections;
using UnityEngine;

namespace Gameplay
{
    public class Weapon : MonoBehaviour
    {
        public event Action<float> OnCooldownChanged;
        public event Action<int> OnCapacityChanged;
        public event Action OnEmptied;

        public string Name => _config.Name;

        [SerializeField]
        private WeaponAppearance _appearance;

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

            _projectileConfig = config.Projectile;

            ResetLevel();
        }

        public void ResetLevel()
        {
            if (_shotCooldown != null)
            {
                StopCoroutine(_shotCooldown);

                _shotCooldown = null;
            }

            SetCapacity(_config.Capacity);
        }

        public void Shoot()
        {
            if (_canShoot)
            {
                _projectileSpawn.Spawn(_appearance.ShotPoint.position, _appearance.ShotPoint.forward, _projectileConfig);

                _remainCapacity--;

                SetCapacity(Mathf.Max(0, _remainCapacity));

                if (_remainCapacity == 0)
                {
                    OnEmptied?.Invoke();
                }

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
            _appearance.gameObject.SetActive(active);
            _canShoot = _isActive || (_remainCapacity > 0);

            if (!active && _remainCooldown >= _config.ShotPeriod)
            {
                gameObject.SetActive(false);
            }
        }

        private void SetCapacity(int newCapacity)
        {
            _remainCapacity = newCapacity;

            OnCapacityChanged?.Invoke(_remainCapacity);
        }

        private IEnumerator CooldownCoroutine(float shotPeriod)
        {
            _canShoot = false;

            while(_remainCooldown < shotPeriod)
            {
                _remainCooldown += Time.deltaTime;

                OnCooldownChanged?.Invoke(_remainCooldown / shotPeriod);

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