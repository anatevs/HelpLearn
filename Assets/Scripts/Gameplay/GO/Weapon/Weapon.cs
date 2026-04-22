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

        public string Name => _weaponParams.Name;

        [SerializeField]
        protected WeaponAppearance _appearance;

        protected WeaponParams _weaponParams;

        private ProjectileConfig _projectileConfig;

        protected ProjectileSpawnService _projectileSpawn;

        private bool _isActive = false;

        private Coroutine _cooldownCoroutine;

        private bool _canShoot = true;

        private int _remainCapacity;

        private float _remainCooldown = 0;

        public void Construct(ProjectileSpawnService spawnService)
        {
            _projectileSpawn = spawnService;
        }

        public void Init(WeaponParams weaponParams)
        {
            _weaponParams = weaponParams;

            _projectileConfig = _weaponParams.Projectile;

            _projectileSpawn.InitProjectileType(_projectileConfig);

            ResetLevel();
        }

        public virtual void ResetLevel()
        {
            if (_cooldownCoroutine != null)
            {
                StopCoroutine(_cooldownCoroutine);

                _cooldownCoroutine = null;
            }

            SetRemain(_weaponParams.Capacity);
        }

        public void Shoot()
        {
            if (_canShoot)
            {
                _projectileSpawn.Spawn(_appearance.ShotPoint.position, _appearance.ShotPoint.forward, _projectileConfig);

                _remainCapacity--;

                SetRemain(Mathf.Max(0, _remainCapacity));

                if (_remainCapacity == 0)
                {
                    OnEmptied?.Invoke();
                }

                _canShoot = _remainCapacity > 0;

                if (_canShoot)
                {
                    _cooldownCoroutine = StartCoroutine(CooldownCoroutine(_weaponParams.ShotPeriod));
                }
            }
        }

        public void ChangeActive(bool active)
        {
            _isActive = active;
            _appearance.gameObject.SetActive(active);

            var isCooldowned = _cooldownCoroutine == null;

            _canShoot = _isActive && (_remainCapacity > 0) && isCooldowned;

            if (!active && isCooldowned)
            {   
                gameObject.SetActive(false);
            }
        }

        protected virtual void SetRemain(int newCapacity)
        {
            _remainCapacity = newCapacity;

            OnCapacityChanged?.Invoke(_remainCapacity);
        }

        private IEnumerator CooldownCoroutine(float shotPeriod)
        {
            _canShoot = false;

            while (_remainCooldown < shotPeriod)
            {
                _remainCooldown += Time.deltaTime;

                OnCooldownChanged?.Invoke(_remainCooldown / shotPeriod);

                yield return null;
            }

            _remainCooldown = 0;
            OnCooldownChanged?.Invoke(1);

            _canShoot = _isActive;

            if (!_isActive)
            {
                gameObject.SetActive(false);
            }

            _cooldownCoroutine = null;
        }
    }
}