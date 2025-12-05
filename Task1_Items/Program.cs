// See https://aka.ms/new-console-template for more information
using Task1_Items;
using Task1_Items.Inits;

Console.WriteLine("Game start!");

var initPlayer = new InitItemsAndPlayer();
var player = initPlayer.Player;

var initEnemy = new InitEnemy();
var enemiesController = initEnemy.EnemiesController;
var enemy = initEnemy.Enemy;

var initBuy = new InitBuyItems();
var initCommands = new InitMainCommands(player, enemiesController, initBuy);

var gameCommands = initCommands.GameCommands;

Fighter winFighter = player;

while (player.HP > 0)
{
    gameCommands.ShowOptions();

    var pressKey = Console.ReadLine();

    if (pressKey == null || !gameCommands.InvokeOption(pressKey))
    {
        Console.WriteLine("Wrong option number entered");
    }

    winFighter = enemy;

    if (enemy.HP == 0)
    {
        enemy.Reward(player);

        if (!enemiesController.ChangeToNext())
        {
            winFighter = player;
            break;
        }

        enemy = enemiesController.CurrentEnemy;
    }
}

Console.WriteLine();
Console.WriteLine($"{winFighter.Name} win! Game end");
return;