using Task1_Items.Items;

namespace Task1_Items.Commands
{
    public class BuyItemCommand<T> : GameCommand where T : IItem
    {
        private readonly MoneyStorage _playerMoney;
        private readonly T _item;
        private readonly ItemsStorage<T> _itemsStorage;

        public BuyItemCommand(MoneyStorage playerMoney, T potion,
            ItemsStorage<T> playerStorage)
        {
            _name = $"Buy potion {potion.Name}, cost = {potion.Cost}";

            _playerMoney = playerMoney;
            _item = potion;
            _itemsStorage = playerStorage;
        }

        public override void Execute()
        {
            if (!_playerMoney.TryChangeMoney(-_item.Cost))
            {
                return;
            }

            var item = (T)_item.Clone();

            _itemsStorage.AddItem(item);

            Console.WriteLine($"Potion {_item.Name} has been bought");
        }
    }
}