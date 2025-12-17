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

        private bool _canShoot = true;

        private void OnEnable()
        {
            _input.ShootClicked += Fire;

            _weapon.ShowActive(_canShoot);
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