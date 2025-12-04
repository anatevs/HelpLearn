using Task1_Items.Items;

namespace Task1_Items
{
    public class Player : Fighter
    {
        public event Action? OnAttacked;

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

        public override void Attack(Fighter target)
        {
            base.Attack(target);

            OnAttacked?.Invoke();
        }

        public void ConsumeItem(IConsumable item)
        {
            item.Consume(this);
        }

        public void EquipItem(IArming item)
        {
            item.Equip(this);
        }

        public void DisarmItem(IArming item)
        {
            item.Disarm(this);
        }
    }
}