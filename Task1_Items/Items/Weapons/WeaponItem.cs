namespace Task1_Items.Items
{
    public class WeaponItem : Item,
        IArming
    {
        private readonly int _damage;

        public WeaponItem(string name, float cost, int damage)
            : base(name, cost)
        {
            _damage = damage;
        }

        public override void Use(Player player)
        {
            player.Disarm();

            Equip(player);
        }

        public override WeaponItem Clone()
        {
            return new WeaponItem(_name, _cost, _damage);
        }

        public virtual void Disarm(Player player)
        {
            player.AddDamage(-_damage);

            Console.WriteLine();
            Console.WriteLine($"{_name} was unarmed, -{_damage} damage for {player.Name}");
        }

        public virtual void Equip(Player player)
        {
            player.SetCurrentWeapon(this);
            player.AddDamage(_damage);

            Console.WriteLine();
            Console.WriteLine($"{_name} was equiped, +{_damage} damage for {player.Name}");
        }
    }
}