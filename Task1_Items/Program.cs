// See https://aka.ms/new-console-template for more information
using Task1_Items;

Console.WriteLine("Game start!");
Console.WriteLine("To stop the game press q");

var player = new Player("Player", 10, 2);

var enemy = new Enemy("Enemy", 6, 1);

var fight = new Fight();

Console.WriteLine("To start fight press Enter");

var fighters = new Fighter[2] { player, enemy };
int currentIndex = -1;
int nextIndex = (currentIndex + 1) % fighters.Length;

while (player.HP > 0 && enemy.HP > 0)
{
    if (Console.KeyAvailable)
    {
        ConsoleKeyInfo keyInfo = Console.ReadKey(true);
        if (keyInfo.Key == ConsoleKey.Enter)
        {
            currentIndex = nextIndex;
            nextIndex = fight.MakeTurn(fighters, currentIndex);

            //currentFighter = fighters[currentIndex];
            //nextFighter = fighters[nextIndex];

            //Console.WriteLine();
            //Console.WriteLine($"{currentFighter.Name} attacks {nextFighter.Name}");

            //currentFighter.MakeDamage(nextFighter);

            //Console.WriteLine($"{player.Name}: HP = {player.HP}, damage = {player.Damage}");
            //Console.WriteLine($"{enemy.Name}: HP = {enemy.HP}, damage = {enemy.Damage}");

            //currentIndex = nextIndex;
            //nextIndex = (currentIndex + 1) % fighters.Length;

            //Console.WriteLine("press Enter to go to a next step");
        }

        else if (keyInfo.Key == ConsoleKey.Q)
        {
            Console.WriteLine("The game stopped");
            return;
        }
    }
}

Console.WriteLine($"{fighters[currentIndex].Name} win! Game end");
return;
