namespace Task2_TaskManager.TaskItems
{
    public static class TasksView
    {
        public static void ShowFiltered(IEnumerable<(int Index, TaskItem Task)> filteredItems, string? filterName)
        {
            Console.WriteLine();
            Console.WriteLine($"{filterName}:");

            foreach (var item in filteredItems)
            {
                ShowTask(item.Index, item.Task);
            }
        }

        public static void ShowTask(int index, TaskItem task)
        {
            Console.WriteLine($"{index + 1}. {task.GetStringInfo()}");
        }
    }
}