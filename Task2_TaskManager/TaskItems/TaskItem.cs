using Task2_TaskManager.Enums;

namespace Task2_TaskManager.TaskItems
{
    public class TaskItem
    {
        public string Name => _name;
        public string? Description => _description;
        public Priority Priority => _priority;
        public Category Category => _category;
        public Status Status
        {
            get => _status;
            set => _status = value;///////////////////////////make changes in dictionary
        }

        private string _name = "";
        private string? _description;
        private Priority _priority;
        private Category _category;
        private Status _status;
        private Dictionary<Type, int> _enums = new();

        public TaskItem(string name,
            string? description,
            Priority priority,
            Category category,
            Status status)
        {
            _name = name;
            _description = description;
            _priority = priority;
            _category = category;
            _status = status;

            _enums.Add(typeof(Priority), (int)_priority);
            _enums.Add(typeof(Category), (int)_category);
            _enums.Add(typeof(Status), (int)_status);
        }

        public int GetEnumValue(Type type)
        {
            return _enums[type];
        }
    }
}