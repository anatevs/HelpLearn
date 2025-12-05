namespace Task1_Items
{
    public class MoneyStorage
    {
        public float Money => _money;

        private float _money;

        public MoneyStorage(float initMoney)
        {
            _money = initMoney;
        }

        public bool TryChangeMoney(float amount)
        {
            if (amount < 0 && _money < -amount)
            {
                Console.WriteLine($"Not enough money: price = {-amount}, money = {_money}");
                return false;
            }

            _money += amount;

            Console.WriteLine($"Money changed; {_money} money in storage");

            return true;
        }
    }
}