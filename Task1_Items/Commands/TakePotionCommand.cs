using Task1_Items.Items;

namespace Task1_Items.Commands
{
    public class TakePotionCommand : GameCommand
    {
        private readonly PotionItem _potion;
        private readonly ItemsStorage<PotionItem> _potionsStorage;

        public TakePotionCommand(PotionItem potion,
            ItemsStorage<PotionItem> storage)
        {
            _name = $"Set active potion {potion.Name}";

            _potion = potion;
            _potionsStorage = storage;
        }

        public override void Execute()
        {
            Console.WriteLine($"{_potion.Name} is selected now");
            if ( _potionsStorage.CurrentActive != null)
            {
                _potionsStorage.AddItem(_potionsStorage.CurrentActive);
            }

            _potionsStorage.CurrentActive = _potion;
            _potionsStorage.RemoveItem(_potion);
        }
    }
}