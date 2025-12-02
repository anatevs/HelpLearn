// See https://aka.ms/new-console-template for more information
using Task1_Items;

Console.WriteLine("Game start!");
Console.WriteLine("To stop the game press q");

var player = new Player("Player", 10, 2);

var enemy = new Enemy("Enemy", 6, 1);

var fight = new Fight();

Console.WriteLine("To start fight press Enter");

var fighters = new Fighter[2] { player, enemy };

Fighter currentFighter = player;

while (player.HP > 0 && enemy.HP > 0)
{
    if (Console.KeyAvailable)
    {
        ConsoleKeyInfo keyInfo = Console.ReadKey(true);
        if (keyInfo.Key == ConsoleKey.Enter)
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

        else if (keyInfo.Key == ConsoleKey.Q)
        {
            Console.WriteLine("The game stopped");
            return;
        }
    }
}

Console.WriteLine($"{currentFighter.Name} win! Game end");
return;
