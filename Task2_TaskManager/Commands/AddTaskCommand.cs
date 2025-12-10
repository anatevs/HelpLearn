using Task2_TaskManager.Enums;
using Task2_TaskManager.TaskItems;

namespace Task2_TaskManager.Commands
{
    public class AddTaskCommand : BaseTasksCommand
    {
        private TaskParams _taskParams = new();

        private GetStringCommand _nameCommand;
        private GetStringNullCommand _descriptionCommand;
        private GetEnumCommand<Priority> _priorityCommand;
        private GetEnumCommand<Category> _categoryCommand;
        private GetEnumCommand<Status> _statusCommand;

        private readonly List<ICommand> _commands = new();

        public AddTaskCommand(TasksList tasksList) : base(tasksList)
        {
            _name = "Add new task";

            _executeText = "Set task's parameters";

            _nameCommand = new GetStringCommand("Enter task name:");
            _descriptionCommand = new GetStringNullCommand("Enter task description (could be empty):");
            _priorityCommand = new GetEnumCommand<Priority>();
            _categoryCommand = new GetEnumCommand<Category>();
            _statusCommand = new GetEnumCommand<Status>();

            _commands.Add(_nameCommand);
            _commands.Add(_descriptionCommand);
            _commands.Add(_priorityCommand);
            _commands.Add(_categoryCommand);
            _commands.Add(_statusCommand);
        }

        public override void Execute()
        {
            base.Execute();

            foreach(var command in _commands)
            {
                command.Execute();
            }

            _taskParams.Name = _nameCommand.Result;
            _taskParams.Description = _descriptionCommand.Result;
            _taskParams.Priority = (Priority)_priorityCommand.EnumIndex;
            _taskParams.Category = (Category)_categoryCommand.EnumIndex;
            _taskParams.Status = (Status)_statusCommand.EnumIndex;


            var task = new TaskItem(_taskParams);
            _tasksList.AddTask(task);
            Console.WriteLine($"{task.GetStringInfo()}");
        }
    }
}