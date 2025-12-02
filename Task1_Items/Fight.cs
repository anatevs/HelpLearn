namespace Task1_Items
{
    public sealed class Fight
    {
        public void MakeMove(Fighter activeFighter, Fighter passiveFighter)
        {
            Console.WriteLine();
            Console.WriteLine($"{activeFighter.Name} attacks {passiveFighter.Name}");

            activeFighter.MakeDamage(passiveFighter);

            Console.WriteLine("press Enter to go to a next step");
        }

        public void ShowFightersInfo(Fighter fighter1, Fighter fighter2)
        {
            fighter1.ShowFighterInfo();
            fighter2.ShowFighterInfo();
        }
    }
}