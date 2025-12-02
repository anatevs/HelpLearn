// See https://aka.ms/new-console-template for more information
using Task1_Items;

Console.WriteLine("Game start!");

var player = new Player("Player", 10, 2);

var enemy = new Enemy("Enemy", 6, 1);

var fight = new Fight();

string[] optionsKeys = [ "1", "q" ];

var gameUI = new GameUI();

gameUI.AddOption(optionsKeys[0], "Attack the enemy");
gameUI.AddOption(optionsKeys[^1], "Quit");

gameUI.ShowOptions();

Fighter currentFighter = player;

while (player.HP > 0 && enemy.HP > 0)
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
    }

    else if (pressKey == "q")
    {
        Console.WriteLine("The game stopped");
        return;
    }
}

Console.WriteLine($"{currentFighter.Name} win! Game end");
return;
