using Task1_Items.Items;

namespace Task1_Items.Commands
{
    public class SelectBuyItemCommand<T> : SelectItemCommand<T> where T : IItem
    {
        private readonly ItemsStorage<T> _playerStorage;

        public SelectBuyItemCommand(Player player,
            ItemsStorage<T> sellerStorage,
            ItemsStorage<T> itemsStorage,
            string itemMenuName)
            : base(player, sellerStorage)
        {
            _name = $"Buy {itemMenuName}";
            _playerStorage = itemsStorage;

            FillOptionsList();
        }

        protected override GameCommand? CreateCommand(T item)
        {
            if (_playerStorage != null)
            {
                return new BuyItemCommand<T>(_player.MoneyStorage, item, _playerStorage);
            }

            return null;
        }
    }
}