using Task2_TaskManager.TaskItems;

namespace Task2_TaskManager.Commands
{
    public class SortPriorityCommand : BaseCommand
    {
        private readonly TasksList _tasksList;

        public SortPriorityCommand(TasksList tasksList)
        {
            _tasksList = tasksList;
        }

        public override void Execute()
        {
            _tasksList.ShowSortPriority();
        }
    }
}