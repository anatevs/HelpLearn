using Task1_Items.Items;

namespace Task1_Items
{
    public class WeaponController
    {
        private readonly WeaponItem[] _weapons = [];

        private WeaponItem _currentActive = new WeaponItem("ZeroWeapon", 0, 0);

        public WeaponController()
        {
            _weapons = [
                new WeaponItem("Weapon-Power1", 2, 1),
                new WeaponItem("Weapon-Power4", 6, 4),
                new WeaponItem("Weapon-Power10", 20, 10)
                    ];
        }

        public void ChangeWeapon(WeaponItem weapon)
        {
            _currentActive = weapon;
        }
    }
}