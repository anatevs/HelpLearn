using Task2_TaskManager.TaskItems;

namespace Task2_TaskManager.Commands
{
    public class FilterCommand<T> : ChooseEnumCommand<T> where T : Enum
    {
        private readonly TasksList _tasksList;

        public FilterCommand(TasksList tasksList) : base()
        {
            _tasksList = tasksList;

            _name = $"Filter by {_enumType.Name.ToLower()}";
        }

        protected override void HandleEnumIndex(int enumIndex)
        {
            IEnumerable<(int Index, TaskItem Task)> indexItems =
                _tasksList.Tasks
                .Select((item, index) => (index, item))
                .Where((x) => x.item.GetEnumValue(_enumType) == enumIndex);

            var enumName = Enum.ToObject(_enumType, enumIndex);
            TasksView.ShowFiltered(indexItems, enumName.ToString());
        }
    }
}