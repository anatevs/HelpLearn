using System.Collections;
using UnityEngine;

namespace GameCore
{
    public class PlayerShooting : MonoBehaviour
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

        private bool _canShoot = true;

        private void OnEnable()
        {
            _input.ShootClicked += Fire;
        }

        private void OnDisable()
        {
            _input.ShootClicked -= Fire;
        }

        private void Fire()
        {
            if (_canShoot)
            {
                _canShoot = false;
                _weapon.ShowActive(_canShoot);

                StartCoroutine(Cooldown(_weaponConfig.FireCooldown));

                _spawner.Spawn(_firePoint.position, _firePoint.rotation,
                    _weaponConfig.ProjectileSpeed);
            }
        }

        private IEnumerator Cooldown(float cooldown)
        {
            yield return new WaitForSeconds(cooldown);

            _canShoot = true;
            _weapon.ShowActive(_canShoot);
        }
    }
}