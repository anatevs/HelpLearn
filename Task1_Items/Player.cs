namespace Task1_Items
{
    public class Player : Fighter
    {
        public event Action? OnStriked;

        public override int Damage => _damage + _addDamage;

        private int _addDamage;

        public Player(string name, int hp, int damage)
            : base(name, hp, damage)
        {

        }

        public void AddDamage(int damage)
        {
            _addDamage += damage;
        }

        public override void Strike(IDamagable target)
        {
            base.Strike(target);

            OnStriked?.Invoke();
        }
    }
}