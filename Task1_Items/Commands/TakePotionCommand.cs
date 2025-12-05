using Task1_Items.Items;

namespace Task1_Items.Commands
{
    public class TakePotionCommand : GameCommand
    {
        private readonly PotionItem _potion;
        private readonly ItemsStorage<PotionItem> _storage;

        public TakePotionCommand(PotionItem potion,
            ItemsStorage<PotionItem> storage)
        {
            _name = $"Set active potion {potion.Name}";

            _potion = potion;
            _storage = storage;
        }

        public override void Execute()
        {
            _storage.CurrentActive = _potion;
        }
    }
}