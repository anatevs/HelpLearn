// See https://aka.ms/new-console-template for more information
using Task1_Items;
using Task1_Items.Commands;
using Task1_Items.Items;

Console.WriteLine("Game start!");

var weapons = new List<WeaponItem>();
weapons.Add(new WeaponItem("Weapon-Power1", 2, 1));
weapons.Add(new WeaponItem("Weapon-Power2", 4, 2));
weapons.Add(new WeaponItem("Weapon-Power3", 6, 3));

var weaponsStorage = new ItemsStorage<WeaponItem>(weapons);

var potions = new List<PotionItem>();
potions.Add(new HPPotion("Potion-hp3", 5, 3));
potions.Add(new BoostPotion("Potion-hp2-boost1x2", 6, 2, 1, 2));

var potionsStorage = new ItemsStorage<PotionItem>(potions);


var player = new Player("Player", 10, 2, weaponsStorage);

var enemy = new Enemy("Enemy", 20, 1);

string[] optionsKeys = [ "1", "2", "3", "4", "5", "q" ];

var gameCommands = new CommandsController();

Fighter winFighter = player;



var cm0 = new PlayerAttackCommand(player, enemy);
var cm1 = new DrinkPotionCommand(player, enemy, potionsStorage);
var cm2 = new SelectWeaponCommand(player, weaponsStorage);
var cm3 = new SelectPotionCommand(player, potionsStorage);
var cm4 = new ShowStateCommand(player, enemy);
var cm5 = new ExitGameCommand();

gameCommands.AddOption(optionsKeys[0], cm0);
gameCommands.AddOption(optionsKeys[1], cm1);
gameCommands.AddOption(optionsKeys[2], cm2);
gameCommands.AddOption(optionsKeys[3], cm3);
gameCommands.AddOption(optionsKeys[4], cm4);
gameCommands.AddOption(optionsKeys[5], cm5);

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
        winFighter = player;
        break;
    }
}

Console.WriteLine();
Console.WriteLine($"{winFighter.Name} win! Game end");
return;