using UnityEngine;

namespace Gameplay
{
    public class BaseWeaponConfig<weaponT> : ScriptableObject
        where weaponT : Weapon
    {
        public WeaponParams WeaponParams => _weaponParams;

        [SerializeField]
        protected WeaponParams _weaponParams;

        [SerializeField]
        private weaponT _prefab;

        public virtual weaponT CreateNewWeapon()
        {
            weaponT weapon = Instantiate(_prefab);

            weapon.Init(_weaponParams);

            return weapon;
        }
    }
}