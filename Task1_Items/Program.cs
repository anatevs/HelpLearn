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

var moneyStorage = new MoneyStorage(10);

var player = new Player("Player", 10, 2, weaponsStorage, moneyStorage);


var enemies = new Queue<Enemy>();

var enemiesController = new EnemiesController();

enemiesController.AddEnemy(new Enemy("Enemy-dmg1-rw5", 10, 1, 5));
enemiesController.AddEnemy(new Enemy("Enemy-dmg2-rw8", 20, 2, 8));
enemiesController.AddEnemy(new Enemy("Enemy-dmg4-rw15", 10, 4, 15));

if (!enemiesController.ChangeToNext())
{
    Console.WriteLine("no enemies in enemies controller!!");
}

var enemy = enemiesController.CurrentEnemy;

string[] optionsKeys = [ "1", "2", "3", "4", "5", "q" ];

var gameCommands = new CommandsController();

Fighter winFighter = player;



var cm0 = new PlayerAttackCommand(player, enemiesController);
var cm1 = new DrinkPotionCommand(player, enemiesController, potionsStorage);
var cm2 = new SelectWeaponCommand(player, weaponsStorage);
var cm3 = new SelectPotionCommand(player, potionsStorage);
var cm4 = new ShowStateCommand(player, enemiesController);
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