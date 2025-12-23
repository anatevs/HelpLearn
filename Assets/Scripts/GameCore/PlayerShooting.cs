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

        public float Cooldown { get => _cooldown; set => _cooldown = value; }
        public float Speed { get => _projectileSpeed; set => _projectileSpeed = value; }

        [SerializeField]
        private InputController _input;

        [SerializeField]
        private WeaponAnimation _weaponAnimation;

        [SerializeField]
        private Transform _firePoint;

        [SerializeField]
        private ProjectileSpawner _spawner;

        [SerializeField]
        private TMP_Text _shotsText;

        [SerializeField]
        private TMP_Text _hitsText;

        [SerializeField]
        private AudioManager _audioManager;

        private readonly List<Projectile> _activeProjectiles = new();

        private bool _canShoot = true;

        private int _shotsCount = 0;

        private int _hitsCount = 0;

        private float _cooldown = 3f;
        private float _projectileSpeed = 200f;

        private void OnEnable()
        {
            _input.ShootClicked += Shoot;

            SetText(_shotsText, _shotsCount);
            SetText(_hitsText, _hitsCount);
        }

        private void OnDisable()
        {
            _input.ShootClicked -= Shoot;

            foreach (var projectile in _activeProjectiles)
            {
                projectile.Destroyed -= OnProjectileDestoyed;
                projectile.Hitted -= OnProjectileHitted;
            }
        }

        private void Shoot()
        {
            if (_canShoot)
            {
                _canShoot = false;
                _weaponAnimation.ShowBodyActive(_canShoot);

                StartCoroutine(MakeCooldown(_cooldown));

                var projectile = _spawner.Spawn(_firePoint.position, _firePoint.rotation,
                    _projectileSpeed);

                projectile.Hitted += OnProjectileHitted;
                projectile.Destroyed += OnProjectileDestoyed;
                _activeProjectiles.Add(projectile);

                _shotsCount++;
                Shooted?.Invoke(_shotsCount);
                SetText(_shotsText, _shotsCount);

                _audioManager.PlayFire();

                _weaponAnimation.ShowRecoil(true);
            }
            else
            {
                Debug.LogWarning("Can't shot as weapon in a cooldown");
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
            _hitsCount++;
            Hitted?.Invoke(_hitsCount);
            SetText(_hitsText, _hitsCount);

            _audioManager.PlayTargetDestroy();
        }

        private IEnumerator MakeCooldown(float cooldown)
        {
            yield return new WaitForSeconds(cooldown);

            _canShoot = true;
            _weaponAnimation.ShowBodyActive(_canShoot);
        }

        private void SetText(TMP_Text text, int value)
        {
            text.text = value.ToString();
        }
    }
}