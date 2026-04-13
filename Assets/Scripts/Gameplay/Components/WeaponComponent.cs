using UnityEngine;

namespace Gameplay
{
    public sealed class WeaponComponent : MonoBehaviour
    {
        [SerializeField]
        private Transform _handPoint;

        private Weapon _currentWeapon;

        public void SetWeapon(Weapon weapon)
        {
            if (_handPoint.childCount > 0)
            {
                var weapons = _handPoint.GetComponents<Weapon>();

                foreach (var item in weapons)
                {
                    item.gameObject.SetActive(false);
                }
            }

            weapon.transform.SetPositionAndRotation(_handPoint.position, _handPoint.rotation);
            weapon.transform.SetParent(_handPoint);

            weapon.gameObject.SetActive(true);

            _currentWeapon = weapon;
        }

        public void Shoot()
        {
            _currentWeapon.Shoot();
        }
    }
}