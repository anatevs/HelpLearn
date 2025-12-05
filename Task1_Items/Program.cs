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
potions.Add(new HPPotion("Potion0-hp3", 0, 2));
potions.Add(new BoostPotion("Potion0-hp2-boost1x2", 0, 2, 1, 2));

var potionsStorage = new ItemsStorage<PotionItem>(potions);

var moneyStorage = new MoneyStorage(10);

var player = new Player("Player", 10, 2, weaponsStorage, potionsStorage, moneyStorage);


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

string[] optionsKeys = [ "1", "2", "3", "4", "5", "q", "6" ];

var gameCommands = new CommandsController();

Fighter winFighter = player;

var potionsBuy = new List<PotionItem>();
potionsBuy.Add(new HPPotion("Potion-hp5", 5, 3));
potionsBuy.Add(new BoostPotion("Potion-hp4-boost4x3", 15, 4, 4, 3));
var potionsBuyStorage = new ItemsStorage<PotionItem>(potionsBuy);


var cm0 = new PlayerAttackCommand(player, enemiesController);
var cm1 = new DrinkPotionCommand(player, enemiesController);
var cm2 = new SelectWeaponCommand(player, weaponsStorage);
var cm3 = new SelectPotionCommand(player, potionsStorage);
var cm4 = new ShowStateCommand(player, enemiesController);
var cm5 = new ExitGameCommand();
var cm6 = new SelectBuyPotionCommand(player, potionsBuyStorage);

gameCommands.AddOption(optionsKeys[0], cm0);
gameCommands.AddOption(optionsKeys[1], cm1);
gameCommands.AddOption(optionsKeys[2], cm2);
gameCommands.AddOption(optionsKeys[3], cm3);
gameCommands.AddOption(optionsKeys[4], cm4);
gameCommands.AddOption(optionsKeys[5], cm5);
gameCommands.AddOption(optionsKeys[6], cm6);

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