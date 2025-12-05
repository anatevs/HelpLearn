namespace Task1_Items.Commands
{
    public class PlayerAttackCommand : GameCommand
    {
        private readonly Player _player;
        private readonly EnemiesController _emiesController;

        public PlayerAttackCommand(Player player, EnemiesController enemiesController)
        {
            _name = "Attack the enemy";

            _player = player;
            _emiesController = enemiesController;
        }

        public override void Execute()
        {
            base.Execute();

            var enemy = _emiesController.CurrentEnemy;

            _player.Attack(enemy);

            if (enemy.HP > 0)
            {
                enemy.Attack(_player);
            }
        }
    }
}