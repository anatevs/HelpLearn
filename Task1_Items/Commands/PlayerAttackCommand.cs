namespace Task1_Items.Commands
{
    public class PlayerAttackCommand : GameCommand
    {
        private readonly Player _player;
        private readonly Enemy _enemy;

        public PlayerAttackCommand(Player player, Enemy enemy)
        {
            _name = "Attack the enemy";

            _player = player;
            _enemy = enemy;
        }

        public override void Execute()
        {
            base.Execute();

            _player.Attack(_enemy);

            if (_enemy.HP > 0)
            {
                _enemy.Attack(_player);
            }
        }
    }
}