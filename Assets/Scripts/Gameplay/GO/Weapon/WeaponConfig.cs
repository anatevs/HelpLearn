using UnityEngine;
using UnityEngine.UI;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "WeaponConfig",
        menuName = "Configs/Weapon")]
    public class WeaponConfig : ScriptableObject
    {
        public string Name => _name;
        public ProjectileConfig Projectile => _projectileConfig;
        public float ShotPeriod => _shotPeriod;
        public int Capacity => _capacity;

        [SerializeField]
        private string _name;

        [SerializeField]
        private ProjectileConfig _projectileConfig;

        [SerializeField]
        private float _shotPeriod;

        [SerializeField]
        private int _capacity;

        [SerializeField]
        private Weapon _prefab;

        public Weapon CreateNewWeapon()
        {
            Weapon weapon = Instantiate(_prefab);

            weapon.Init(this);

            return weapon;
        }
    }
}