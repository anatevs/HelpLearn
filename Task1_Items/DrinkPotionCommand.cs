using Task1_Items.Items;

namespace Task1_Items.Commands
{
    public class DrinkPotionCommand : GameCommand
    {
        private readonly Player _player;
        private readonly Enemy _enemy;
        private PotionItem _potion;

        public DrinkPotionCommand(Player player, Enemy enemy, PotionItem potion)
        {
            _name = "Drink potion";

            _player = player;
            _enemy = enemy;
            _potion = potion;
        }

        public override void Execute()
        {
            base.Execute();

            _potion.Use(_player);

            _enemy.Attack(_player);
        }

        public void ChangePotion(PotionItem potion)
        {
            _potion = potion;
        }
    }
}