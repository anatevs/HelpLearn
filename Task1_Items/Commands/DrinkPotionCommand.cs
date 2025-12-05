using Task1_Items.Items;

namespace Task1_Items.Commands
{
    public class DrinkPotionCommand : GameCommand
    {
        private readonly Player _player;
        private readonly EnemiesController _enemiesController;
        private readonly ItemsStorage<PotionItem> _storage;

        public DrinkPotionCommand(Player player, EnemiesController enemiesController,
            ItemsStorage<PotionItem> storage)
        {
            _name = "Drink potion";

            _player = player;
            _enemiesController = enemiesController;
            _storage = storage;
        }

        public override void Execute()
        {
            base.Execute();

            _storage.CurrentActive?.Use(_player);

            _enemiesController.CurrentEnemy.Attack(_player);
        }
    }
}