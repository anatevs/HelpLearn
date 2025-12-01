// See https://aka.ms/new-console-template for more information
using Task1_Items;

Console.WriteLine("Game start!");
Console.WriteLine("To stop the game press q");

var player = new Fighter("Player", 10, 2);

var enemy = new Fighter("Enemy", 6, 1);

Console.WriteLine("To start fight press Enter");

var fighters = new Fighter[2] { player, enemy };
int currentIndex = 0;
int nextIndex = (currentIndex + 1) % fighters.Length;

var currentFighter = fighters[currentIndex];
var nextFighter = fighters[nextIndex];

while (player.HP > 0 && enemy.HP > 0)
{
    if (Console.KeyAvailable)
    {
        ConsoleKeyInfo keyInfo = Console.ReadKey(true);
        if (keyInfo.Key == ConsoleKey.Enter)
        {
            currentFighter = fighters[currentIndex];
            nextFighter = fighters[nextIndex];

            Console.WriteLine();
            Console.WriteLine($"{currentFighter.Name} attacks {nextFighter.Name}");

            currentFighter.MakeDamage(nextFighter);

            Console.WriteLine($"{player.Name}: HP = {player.HP}, damage = {player.Damage}");
            Console.WriteLine($"{enemy.Name}: HP = {enemy.HP}, damage = {enemy.Damage}");

            currentIndex = nextIndex;
            nextIndex = (currentIndex + 1) % fighters.Length;

            Console.WriteLine("press Enter to go to a next step");
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
