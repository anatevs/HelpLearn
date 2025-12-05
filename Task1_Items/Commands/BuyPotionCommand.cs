using Task1_Items.Items;

namespace Task1_Items.Commands
{
    public class BuyPotionCommand : GameCommand
    {
        private readonly MoneyStorage _playerMoney;
        private readonly PotionItem _potion;
        private readonly ItemsStorage<PotionItem> _potionsStorage;

        public BuyPotionCommand(MoneyStorage playerMoney, PotionItem potion,
            ItemsStorage<PotionItem> playerStorage)
        {
            _name = $"Buy potion {potion.Name}, cost = {potion.Cost}";

            _playerMoney = playerMoney;
            _potion = potion;
            _potionsStorage = playerStorage;
        }

        public override void Execute()
        {
            if (!_playerMoney.TryChangeMoney(-_potion.Cost))
            {
                return;
            }

            var potion = _potion.Clone();

            _potionsStorage.AddItem(potion);

            Console.WriteLine($"Potion {_potion.Name} has been bought");
        }
    }
}