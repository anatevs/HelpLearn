namespace Task1_Items.Commands
{
    public class ExitGameCommand : GameCommand
    {
        public ExitGameCommand()
        {
            _name = "Quit game";
        }

        public override void Execute()
        {
            base.Execute();

            Environment.Exit(0);
        }
    }
}