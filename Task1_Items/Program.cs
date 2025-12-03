// See https://aka.ms/new-console-template for more information
using Task1_Items;
using Task1_Items.Items;

Console.WriteLine("Game start!");

var player = new Player("Player", 10, 2);

var enemy = new Enemy("Enemy", 6, 1);

var fight = new Fight();

var weapon = new WeaponItem("weapon1", 10, 1);

string[] optionsKeys = [ "1", "2", "q" ];

var gameUI = new GameUI();

gameUI.AddOption(optionsKeys[0], "Attack the enemy");
gameUI.AddOption(optionsKeys[1], "Take weapon");
gameUI.AddOption(optionsKeys[^1], "Quit");

Fighter currentFighter = player;

while (player.HP > 0)
{
    gameUI.ShowOptions();

    var pressKey = Console.ReadLine();

    if (pressKey == optionsKeys[0])
    {
        currentFighter = player;
        fight.MakeMove(player, enemy);
        fight.ShowFightersInfo(player, enemy);

        if (enemy.HP > 0)
        {
            currentFighter = enemy;
            fight.MakeMove(enemy, player);
            fight.ShowFightersInfo(player, enemy);
        }
        else
        {
            //enemy killed
            break;
        }
    }

    else if (pressKey == optionsKeys[1])
    {
        weapon.Use(player);


        currentFighter = enemy;
        fight.MakeMove(enemy, player);
        fight.ShowFightersInfo(player, enemy);
    }

    else if (pressKey == "q")
    {
        Console.WriteLine("The game stopped");
        return;
    }
}

Console.WriteLine();
Console.WriteLine($"{currentFighter.Name} win! Game end");
return;
