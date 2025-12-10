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
            IEnumerable<(int Index, TaskItem Task)> filteredItems =
                _tasksList.Tasks
                .Select((item, index) => (index, item))
                .OrderBy((x) => x.item.Priority);

            TasksView.ShowFiltered(filteredItems, "Sorted by priority");
        }
    }
}