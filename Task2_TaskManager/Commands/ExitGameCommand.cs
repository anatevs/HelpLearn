using Task2_TaskManager.Inits;

namespace Task2_TaskManager.Commands
{
    public class ExitGameCommand : BaseCommand
    {
        private readonly SaveLoadInit _saveLoad;

        public ExitGameCommand(SaveLoadInit saveLoad)
        {
            _name = "Quit game";
            _saveLoad = saveLoad;
        }

        public override void Execute()
        {
            base.Execute();

            _saveLoad.Save();

            Environment.Exit(0);
        }
    }
}