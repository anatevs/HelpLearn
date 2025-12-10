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
            _tasksList.FilterByEnum(_enumType, enumIndex);
        }
    }
}