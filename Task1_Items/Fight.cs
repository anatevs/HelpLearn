namespace Task1_Items
{
    public class Fight
    {
        public int MakeTurn(Fighter[] fighters, int currentIndex)
        {
            var nextIndex = (currentIndex + 1) % fighters.Length;

            var currentFighter = fighters[currentIndex];
            var nextFighter = fighters[nextIndex];

            Console.WriteLine();
            Console.WriteLine($"{currentFighter.Name} attacks {nextFighter.Name}");

            currentFighter.MakeDamage(nextFighter);

            ShowFightersInfo(fighters);

            Console.WriteLine("press Enter to go to a next step");

            return nextIndex;
        }

        private void ShowFightersInfo(Fighter[] fighters)
        {
            foreach ( Fighter fighter in fighters)
            {
                Console.WriteLine($"{fighter.Name}: HP = {fighter.HP}, damage = {fighter.Damage}");
            }
        }
    }
}