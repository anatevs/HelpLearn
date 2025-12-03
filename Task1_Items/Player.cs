namespace Task1_Items
{
    public class Player : Fighter
    {
        public override int Damage => _damage + _weaponDamage;

        public int WeaponDamage => _weaponDamage;

        private int _weaponDamage;

        public Player(string name, int hp, int damage)
            : base(name, hp, damage)
        {

        }

        public void AddWeaponDamage(int damage)
        {
            _weaponDamage = damage;
        }
    }
}