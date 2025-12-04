// See https://aka.ms/new-console-template for more information
using Task1_Items;
using Task1_Items.Commands;
using Task1_Items.Items;

Console.WriteLine("Game start!");

var player = new Player("Player", 10, 2);

var enemy = new Enemy("Enemy", 6, 1);

var fight = new Fight();

var weapon = new WeaponItem("weapon1", 10, 1);

var potion = new HPPotion("HPPotion", 5, 3);

var boostPotion = new BoostPotion("BoostPotion", 6, 2, 1, 2);

string[] optionsKeys = [ "1", "2", "3", "4", "5", "q" ];

var gameUI = new CommandsController();

Fighter winFighter = player;

var cm0 = new PlayerAttackCommand(player, enemy);
var cm1 = new DrinkPotionCommand(player, enemy, potion);
var cm2 = new TakeWeaponCommand(player, weapon);

var cm4 = new ShowStateCommand(player, enemy);
var cm5 = new ExitGameCommand();

gameUI.AddOption(optionsKeys[0], cm0);
gameUI.AddOption(optionsKeys[1], cm1);
gameUI.AddOption(optionsKeys[2], cm2);

gameUI.AddOption(optionsKeys[4], cm4);
gameUI.AddOption(optionsKeys[5], cm5);

while (player.HP > 0)
{
    gameUI.ShowOptions();

    var pressKey = Console.ReadLine();

    if (pressKey == null || !gameUI.InvokeOption(pressKey))
    {
        Console.WriteLine("Wrong option number entered");
    }

    winFighter = enemy;

    if (enemy.HP == 0)
    {
        winFighter = player;
        break;
    }
}

Console.WriteLine();
Console.WriteLine($"{winFighter.Name} win! Game end");
return;