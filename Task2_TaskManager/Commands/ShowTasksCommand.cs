using Task2_TaskManager.TaskItems;

namespace Task2_TaskManager.Commands
{
    public class ShowTasksCommand : BaseTasksCommand
    {
        public ShowTasksCommand(TasksList tasks) : base(tasks)
        {
            _name = "Show all tasks";
        }

        public override void Execute()
        {
            _tasksList.ShowAll();
        }
    }
}