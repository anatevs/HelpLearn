using Task2_TaskManager.TaskItems;

namespace Task2_TaskManager.Commands
{
    public class SortPriorityCommand : BaseTasksCommand
    {
        public SortPriorityCommand(TasksList tasksList) : base(tasksList)
        {
            _name = "Sort tasks by priority";
        }

        public override void Execute()
        {
            _tasksList.ShowSortPriority();
        }
    }
}