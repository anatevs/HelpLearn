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

        private string _name = "";
        private string? _description;
        private Priority _priority;
        private Category _category;
        private Status _status;


    }
}