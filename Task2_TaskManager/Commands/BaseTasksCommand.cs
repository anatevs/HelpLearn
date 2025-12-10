using Task2_TaskManager.TaskItems;

namespace Task2_TaskManager.Commands
{
    public class BaseTasksCommand : BaseCommand
    {
        protected readonly TasksList _tasksList;

        public BaseTasksCommand(TasksList tasksList) : base()
        {
            _tasksList = tasksList;
        }
    }
}