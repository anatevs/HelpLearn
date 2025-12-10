using Task2_TaskManager.Enums;

namespace Task2_TaskManager.TaskItems
{
    public class TaskItem
    {
        public string Name => _name;
        public string? Description => _description;
        public Priority Priority => _priority;
        public Category Category => _category;
        public Status Status => _status;

        private readonly string _name = "";
        private readonly string? _description;
        private readonly Priority _priority;
        private readonly Category _category;
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

            SetEnums();
        }

        public TaskItem(TaskParams taskParams)
        {
            _name = taskParams.Name;
            _description = taskParams.Description;
            _priority = taskParams.Priority;
            _category = taskParams.Category;
            _status = taskParams.Status;

            SetEnums();
        }

        public int GetEnumValue(Type type)
        {
            return _enums[type];
        }

        public string GetStringInfo()
        {
            var description = (Description == null || Description == "") ? "" : $" ({Description})";

            return $"{Name}{description}\nPriority: {Priority} | Category: {Category} | Status: {Status}";
        }

        public void SetDone()
        {
            _status = Status.Done;
            _enums[typeof(Status)] = (int)Status.Done;
        }

        private void SetEnums()
        {
            _enums.Add(typeof(Priority), (int)_priority);
            _enums.Add(typeof(Category), (int)_category);
            _enums.Add(typeof(Status), (int)_status);
        }
    }
}