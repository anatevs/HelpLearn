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

        public bool IsEnoughMoney(float price)
        {
            return _money >= price;
        }

        public void ChangeMoney(float amount)
        {
            _money += amount;

            Console.WriteLine($"{_money} money in storage");
        }
    }
}