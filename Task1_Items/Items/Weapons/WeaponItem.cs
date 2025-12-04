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
            Disarm(player);

            Equip(player);
        }

        public virtual void Disarm(Player player)
        {
            if (_player == player)
            {
                _player.AddDamage(-_damage);

                Console.WriteLine();
                Console.WriteLine($"{_name} was unarmed, -{_damage} damage for {_player.Name}");

                _player = null;
            }
        }

        public virtual void Equip(Player player)
        {
            _player = player;

            player.AddDamage(_damage);

            Console.WriteLine();
            Console.WriteLine($"{_name} was equiped, +{_damage} damage for {player.Name}");
        }
    }
}