using Task2_TaskManager.Enums;

namespace Task2_TaskManager.TaskItems
{
    public struct TaskParams
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public Priority Priority { get; set; }
        public Category Category { get; set; }
        public Status Status { get; set; }

        public TaskParams()
        {
            Name = "";
            Description = null;
            Priority = default;
            Category = default;
            Status = default;
        }

        public TaskParams(string name, string? description,
            Priority priority, Category category, Status status)
        {
            Name = name;
            Description = description;
            Priority = priority;
            Category = category;
            Status = status;
        }
    }
}