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

        public virtual weaponT CreateNewWeapon(ProjectileSpawnService projectileSpawn)
        {
            weaponT weapon = Instantiate(_prefab);

            weapon.Construct(projectileSpawn);

            weapon.Init(_weaponParams);

            return weapon;
        }
    }
}