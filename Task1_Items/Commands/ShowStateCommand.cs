using Task1_Items.Commands;

namespace Task1_Items
{
    public class ShowStateCommand : GameCommand
    {
        private readonly Player _player;
        private readonly EnemiesController _enemiesController;

        public ShowStateCommand(Player player, EnemiesController enemiesController)
        {
            _name = "Show state";

            _player = player;
            _enemiesController = enemiesController;
        }

        public override void Execute()
        {
            Console.WriteLine();
            _player.ShowFighterInfo();
            _enemiesController.CurrentEnemy.ShowFighterInfo();
        }
    }
}