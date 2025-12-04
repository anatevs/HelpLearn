using Task1_Items.Commands;

namespace Task1_Items
{
    public class ShowStateCommand : GameCommand
    {
        private readonly Player _player;
        private Enemy _enemy;

        public ShowStateCommand(Player player, Enemy enemy)
        {
            _name = "Show state";

            _player = player;
            _enemy = enemy;
        }

        public override void Execute()
        {
            Console.WriteLine();
            _player.ShowFighterInfo();
            _enemy.ShowFighterInfo();
        }

        public void ChangeEnemy(Enemy enemy)
        {
            _enemy = enemy;
        }
    }
}