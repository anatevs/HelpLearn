using Task1_Items.Commands;
using Task1_Items.Items;

namespace Task1_Items.Inits
{
    public class InitMainCommands
    {
        public CommandsController GameCommands => _gameCommands;

        private readonly CommandsController _gameCommands = new();

        public InitMainCommands(Player player, EnemiesController enemiesController, InitBuyItems buyItems)
        {
            string[] optionsKeys = ["1", "2", "3", "4", "5", "6", "7", "q"];

            var cm = new ICommand[optionsKeys.Length];

            cm[0] = new PlayerAttackCommand(player, enemiesController);
            cm[1] = new DrinkPotionCommand(player, enemiesController);
            cm[2] = new SelectWeaponCommand(player, player.WeaponsStorage);
            cm[3] = new SelectPotionCommand(player, player.PotionsStorage);
            cm[4] = new ShowStateCommand(player, enemiesController);
            cm[5] = new SelectBuyItemCommand<PotionItem>(player, buyItems.Potions, player.PotionsStorage, "potion");
            cm[6] = new SelectBuyItemCommand<WeaponItem>(player, buyItems.Weapons, player.WeaponsStorage, "weapon");
            cm[7] = new ExitGameCommand();

            for (int i = 0; i < optionsKeys.Length; i++)
            {
                _gameCommands.AddOption(optionsKeys[i], cm[i]);
            }
        }
    }
}