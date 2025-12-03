namespace Task1_Items
{
    public abstract class Fighter :
        IDamagable,
        IAttacker
    {
        public string Name => _name;

        public int HP { get => _hp; set => _hp = Math.Max(0, value); }
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

        public virtual void MakeDamage(IDamagable target)
        {
            target.HP -= Damage;

            if (target.HP == 0)
            {
                target.Kill();
            }
        }

        public void Kill()
        {
            Console.WriteLine($"{Name} has been killed");
        }


        public void ShowFighterInfo()
        {
            Console.WriteLine($"{Name}: HP = {HP}, damage = {Damage}");
        }
    }
}