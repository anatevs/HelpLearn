namespace Task1_Items
{
    public abstract class Fighter :
        IDamagable,
        IAttacker
    {
        public string Name => _name;

        public int HP { get => _hp; set => _hp = Math.Max(0, value); }
        public int Damage { get => _damage; set => _damage = Math.Max(0, value); }

        private readonly string _name;

        private int _hp;

        private int _damage;

        public Fighter(string name, int hp, int damage)
        {
            _name = name;
            HP = hp;
            Damage = damage;

            Console.WriteLine($"{_name} has been created. It has {HP} hp and {Damage} damage");
        }

        public void MakeDamage(IDamagable target)
        {
            target.HP -= _damage;

            if (target.HP == 0)
            {
                target.Kill();
            }
        }

        public void Kill()
        {
            Console.WriteLine($"{_name} has been killed");
        }

    }
}