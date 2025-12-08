namespace Task2_TaskManager.Commands
{
    public class ExitGameCommand : BaseCommand
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