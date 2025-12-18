using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace GameCore
{
    public sealed class PlayerShooting : MonoBehaviour
    {
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

        private List<Projectile> _activeProjectiles = new();

        private bool _canShoot = true;

        private int _shootCount = 0;

        private int _hitCount = 0;

        private void OnEnable()
        {
            _input.ShootClicked += Fire;
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
                _shootsText.text = _shootCount.ToString();
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
            _hitsText.text = _hitCount.ToString();
        }

        private IEnumerator Cooldown(float cooldown)
        {
            yield return new WaitForSeconds(cooldown);

            _canShoot = true;
            _weapon.ShowActive(_canShoot);
        }
    }
}