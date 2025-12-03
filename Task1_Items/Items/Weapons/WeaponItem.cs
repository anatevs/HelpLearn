namespace Task1_Items.Items
{
    public class WeaponItem : Item,
        IArming
    {
        private readonly int _damage;

        private Player? _player;

        public WeaponItem(string name, float cost, int damage)
            : base(name, cost)
        {
            _damage = damage;
        }

        public override void Use(Player player)
        {
            Disarm();

            Equip(player);
        }

        public virtual void Disarm()
        {
            if (_player != null)
            {
                _player.AddDamage(-_damage);

                Console.WriteLine();
                Console.WriteLine($"{_name} was disarmed, -{_damage} damage from {_player.Name}");

                _player = null;
            }
        }

        public virtual void Equip(Player player)
        {
            _player = player;

            player.AddDamage(_damage);

            Console.WriteLine();
            Console.WriteLine($"{_name} was equiped, +{_damage} damage to {player.Name}");
        }
    }
}