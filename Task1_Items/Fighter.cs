namespace Task1_Items
{
    public abstract class Fighter :
        IDamagable,
        IAttacker
    {
        public string Name => _name;

        public int HP { get => _hp; private set => _hp = Math.Max(0, value); }
        public virtual int Damage => _damage;

        private readonly string _name;

        protected int _hp;

        protected int _damage;

        public Fighter(string name, int hp, int damage)
        {
            _name = name;
            HP = hp;
            _damage = damage;

            Console.WriteLine($"{_name} has been created. It has {HP} hp and {Damage} damage");
        }

        public virtual void GetDamage(int damage)
        {
            HP -= damage;

            Console.WriteLine();
            Console.WriteLine($"{_name} health -{damage}");

            if (HP == 0)
            {
                Kill();
            }
        }

        public virtual void Heal(int hp)
        {
            HP += hp;

            Console.WriteLine($"{_name} health +{hp}");
        }

        public virtual void Attack(Fighter target)
        {
            Console.WriteLine();
            Console.WriteLine($"{_name} attacks {target.Name}");

            target.GetDamage(Damage);
        }

        public virtual void Kill()
        {
            Console.WriteLine($"{_name} has been killed");
        }

        public void ShowFighterInfo()
        {
            Console.WriteLine(GetInfoString());
        }

        protected virtual string GetInfoString()
        {
            return $"{_name}: HP = {HP}, damage = {Damage}";
        }
    }
}