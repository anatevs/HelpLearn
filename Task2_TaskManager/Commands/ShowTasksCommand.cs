using Task2_TaskManager.TaskItems;

namespace Task2_TaskManager.Commands
{
    public class ShowTasksCommand : BaseCommand
    {
        private readonly TasksList _tasks;

        public ShowTasksCommand(TasksList tasks)
        {
            _name = "Show all tasks";
            _tasks = tasks;
        }

        public override void Execute()
        {
            _tasks.ShowAll();
        }
    }
}