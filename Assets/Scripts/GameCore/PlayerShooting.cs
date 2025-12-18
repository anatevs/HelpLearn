using System.Collections;
using UnityEngine;

namespace GameCore
{
    public class PlayerShooting : MonoBehaviour
    {
        [SerializeField]
        private float _shootDelay = 2f;

        [SerializeField]
        private InputController _input;

        [SerializeField]
        private WeaponBody _weapon;

        [SerializeField]
        private Transform _firePoint;

        [SerializeField]
        private ProjectileSpawner _spawner;

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

                StartCoroutine(Cooldown());

                _spawner.Spawn(_firePoint.position, _firePoint.rotation);
            }
        }

        private IEnumerator Cooldown()
        {
            yield return new WaitForSeconds(_shootDelay);

            _canShoot = true;
            _weapon.ShowActive(_canShoot);
        }
    }
}