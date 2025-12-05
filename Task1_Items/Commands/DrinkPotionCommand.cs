using Task1_Items.Items;

namespace Task1_Items.Commands
{
    public class DrinkPotionCommand : GameCommand
    {
        private readonly Player _player;
        private readonly EnemiesController _enemiesController;
        private readonly ItemsStorage<PotionItem> _storage;

        public DrinkPotionCommand(Player player, EnemiesController enemiesController)
        {
            _name = "Drink potion";

            _player = player;
            _enemiesController = enemiesController;
            _storage = player.PotionsStorage;
        }

        public override void Execute()
        {
            base.Execute();

            if (_storage.CurrentActive == null)
            {
                Console.WriteLine("No active potion in player. Select one");
                return;
            }
            else
            {
                _storage.CurrentActive.Use(_player);
                _storage.CurrentActive = null;
            }

            _enemiesController.CurrentEnemy.Attack(_player);
        }
    }
}