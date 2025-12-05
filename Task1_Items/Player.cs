using Task1_Items.Items;

namespace Task1_Items
{
    public class Player : Fighter
    {
        public event Action? OnAttacked;

        public override int Damage => _damage + _addDamage;

        private readonly ItemsStorage<WeaponItem> _weaponsStorage;

        private readonly MoneyStorage _moneyStorage;

        private int _addDamage = 0;

        public Player(string name, int hp, int damage,
            ItemsStorage<WeaponItem> weaponsStorage,
            MoneyStorage moneyStorage)
            : base(name, hp, damage)
        {
            _weaponsStorage = weaponsStorage;
            _moneyStorage = moneyStorage;
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

        public void UseItem(IItem item)
        {
            item.Use(this);
        }

        public void SetCurrentWeapon(WeaponItem weapon)
        {
            _weaponsStorage.CurrentActive = weapon;
        }

        public void Disarm()
        {
            _weaponsStorage.CurrentActive?.Disarm(this);
            _weaponsStorage.CurrentActive = null;
        }

        public void GetReward(float amount)
        {
            _moneyStorage.ChangeMoney(amount);
        }

        protected override string GetInfoString()
        {
            var baseString = base.GetInfoString();

            return $"{baseString}, money: {_moneyStorage.Money}";
        }
    }
}