using Task2_TaskManager.Inits;

namespace Task2_TaskManager.Commands
{
    public class LoadCommand : BaseCommand
    {
        private readonly SaveLoadInit _saveLoad;

        public LoadCommand(SaveLoadInit saveLoad) : base()
        {
            _name = "Load saved tasks";
            _saveLoad = saveLoad;
        }

        public override void Execute()
        {
            _saveLoad.Load();
        }
    }
}