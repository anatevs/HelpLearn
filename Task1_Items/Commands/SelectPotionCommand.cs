using Task1_Items.Items;

namespace Task1_Items.Commands
{
    public class SelectPotionCommand : SelectItemCommand<PotionItem>
    {
        public SelectPotionCommand(Player player, ItemsStorage<PotionItem> storage)
            : base(player, storage)
        {
            _name = "Select active potion";

            _storage.CurrentActive = _storage.Items[0];
        }

        protected override GameCommand CreateCommand(PotionItem item)
        {
            return new TakePotionCommand(item, _storage);
        }
    }
}