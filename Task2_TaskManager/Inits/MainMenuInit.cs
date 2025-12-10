using Task2_TaskManager.Commands;
using Task2_TaskManager.Enums;
using Task2_TaskManager.TaskItems;

namespace Task2_TaskManager.Inits
{
    public class MainMenuInit
    {
        private readonly CommandsController _commandsController = new();

        private readonly TasksList _tasksList;

        private readonly SaveLoadInit _saveLoad;

        public MainMenuInit(TasksList tasksList)
        {
            _tasksList = tasksList;
            _saveLoad = new SaveLoadInit(tasksList);
        }

        public CommandsController Init()
        {
            _commandsController.AddOption("1", new AddTaskCommand(_tasksList));
            _commandsController.AddOption("2", new ShowTasksCommand(_tasksList));
            _commandsController.AddOption("3", new FilterCommand<Status>(_tasksList));
            _commandsController.AddOption("4", new FilterCommand<Category>(_tasksList));
            _commandsController.AddOption("5", new SetDoneCommand(_tasksList));
            _commandsController.AddOption("6", new RemoveTaskCommand(_tasksList));
            _commandsController.AddOption("7", new LoadCommand(_saveLoad));
            _commandsController.AddOption("8", new SortPriorityCommand(_tasksList));
            _commandsController.AddOption("9", new ShowStatusStatistic(_tasksList));
            _commandsController.AddOption("q", new ExitGameCommand(_saveLoad));

            return _commandsController;
        }
    }
}