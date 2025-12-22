using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using GameManagement;
using System;

namespace GameCore
{
    public sealed class PlayerShooting : MonoBehaviour
    {
        public event Action<int> Shooted;

        public event Action<int> Hitted;

        [SerializeField]
        private InputController _input;

        [SerializeField]
        private WeaponBody _weapon;

        [SerializeField]
        private Transform _firePoint;

        [SerializeField]
        private ProjectileSpawner _spawner;

        [SerializeField]
        private WeaponConfig _weaponConfig;

        [SerializeField]
        private TMP_Text _shootsText;

        [SerializeField]
        private TMP_Text _hitsText;

        [SerializeField]
        private AudioManager _audioManager;

        private readonly List<Projectile> _activeProjectiles = new();

        private bool _canShoot = true;

        private int _shootCount = 0;

        private int _hitCount = 0;

        private void OnEnable()
        {
            _input.ShootClicked += Fire;

            SetText(_shootsText, _shootCount);
            SetText(_hitsText, _hitCount);
        }

        private void OnDisable()
        {
            _input.ShootClicked -= Fire;

            foreach (var projectile in _activeProjectiles)
            {
                projectile.Destroyed -= OnProjectileDestoyed;
                projectile.Hitted -= OnProjectileHitted;
            }
        }

        private void Fire()
        {
            if (_canShoot)
            {
                _canShoot = false;
                _weapon.ShowActive(_canShoot);

                StartCoroutine(Cooldown(_weaponConfig.FireCooldown));

                var projectile = _spawner.Spawn(_firePoint.position, _firePoint.rotation,
                    _weaponConfig.ProjectileSpeed);

                projectile.Hitted += OnProjectileHitted;
                projectile.Destroyed += OnProjectileDestoyed;
                _activeProjectiles.Add(projectile);

                _shootCount++;
                Shooted?.Invoke(_shootCount);
                SetText(_shootsText, _shootCount);

                _audioManager.PlayFire();
            }
        }

        private void OnProjectileDestoyed(Projectile projectile)
        {
            _spawner.Unspawn(projectile);
            _activeProjectiles.Remove(projectile);

            projectile.Destroyed -= OnProjectileDestoyed;
            projectile.Hitted -= OnProjectileHitted;
        }

        private void OnProjectileHitted()
        {
            _hitCount++;
            Hitted?.Invoke(_hitCount);
            SetText(_hitsText, _hitCount);

            _audioManager.PlayTargetDestroy();
        }

        private IEnumerator Cooldown(float cooldown)
        {
            yield return new WaitForSeconds(cooldown);

            _canShoot = true;
            _weapon.ShowActive(_canShoot);
        }

        private void SetText(TMP_Text text, int value)
        {
            text.text = value.ToString();
        }
    }
}