using Task1_Items.Items;

namespace Task1_Items.Commands
{
    public class DrinkPotionCommand : GameCommand
    {
        private readonly Player _player;
        private readonly Enemy _enemy;
        private readonly ItemsStorage<PotionItem> _storage;

        public DrinkPotionCommand(Player player, Enemy enemy,
            ItemsStorage<PotionItem> storage)
        {
            _name = "Drink potion";

            _player = player;
            _enemy = enemy;
            _storage = storage;
        }

        public override void Execute()
        {
            base.Execute();

            _storage.CurrentActive?.Use(_player);

            _enemy.Attack(_player);
        }
    }
}