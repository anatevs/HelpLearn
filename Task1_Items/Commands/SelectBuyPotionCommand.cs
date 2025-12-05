using Task1_Items.Items;

namespace Task1_Items.Commands
{
    public class SelectBuyPotionCommand : SelectItemCommand<PotionItem>
    {
        public SelectBuyPotionCommand(Player player,
            ItemsStorage<PotionItem> sellerStorage)
            : base(player, sellerStorage)
        {
            _name = "Select potion to buy";
        }

        protected override GameCommand CreateCommand(PotionItem item)
        {
            return new BuyPotionCommand(_player.MoneyStorage, item, _player.PotionsStorage);
        }
    }
}