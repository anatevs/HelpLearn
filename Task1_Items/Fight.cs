namespace Task1_Items
{
    public sealed class Fight
    {
        public void MakeMove(Fighter activeFighter, Fighter passiveFighter)
        {
            activeFighter.Attack(passiveFighter);
        }

        public void ShowFightersInfo(Fighter fighter1, Fighter fighter2)
        {
            fighter1.ShowFighterInfo();
            fighter2.ShowFighterInfo();
        }
    }
}