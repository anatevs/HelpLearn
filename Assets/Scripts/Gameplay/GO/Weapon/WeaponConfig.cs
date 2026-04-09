using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "WeaponConfig",
        menuName = "Configs/Weapons/PlayerWeapon")]
    public class WeaponConfig : BaseWeaponConfig<Weapon>
    {
        public string Name => _weaponParams.Name;
        public ProjectileConfig Projectile => _weaponParams.Projectile;
        public int Capacity => _weaponParams.Capacity;
    }
}