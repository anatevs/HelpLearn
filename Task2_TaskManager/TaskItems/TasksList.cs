using System;
using Task2_TaskManager.Enums;

namespace Task2_TaskManager.TaskItems
{
    public class TasksList
    {
        private readonly List<TaskItem> _tasks = new();

        public void AddTask(TaskItem item)
        {
            _tasks.Add(item);
        }

        public void AddTasks(IEnumerable<TaskItem> tasks)
        {
            foreach (TaskItem item in tasks)
            {
                _tasks.Add(item);
            }
        }

        public bool TryRemoveTask(int number)
        {
            if (number < 1 || number > _tasks.Count)
            {
                return false;
            }

            _tasks.RemoveAt(number - 1);
            return true;
        }

        public void ShowList()
        {
            for (int i = 0; i < _tasks.Count; i++)
            {
                ShowTask(i + 1, _tasks[i]);
            }
        }

        public void FilterByEnum(Type enumType, int enumValue)
        {
            IEnumerable<(int Index, TaskItem Task)> indexItems =
                _tasks
                .Select((item, index) => (index, item))
                .Where((x) => x.item.GetEnumValue(enumType) == enumValue);
            
            var enumName = Enum.ToObject(enumType, enumValue);
            ShowFiltered(indexItems, enumName.ToString());
        }

        public void ShowSortPriority()
        {
            IEnumerable<(int Index, TaskItem Task)> filteredItems =
                _tasks
                .Select((item, index) => (index, item))
                .OrderBy((x) => x.item.Priority);

            ShowFiltered(filteredItems, "Sorted by priority");
        }

        private void ShowFiltered(IEnumerable<(int Index, TaskItem Task)> filteredItems, string? filterName)
        {
            Console.WriteLine();
            Console.WriteLine($"{filterName}:");
            ShowItems(filteredItems);
        }

        private void ShowItems(IEnumerable<(int Index, TaskItem Task)> selected)
        {
            foreach (var item in selected)
            {
                ShowTask(item.Index, item.Task);
            }
        }

        private void ShowTask(int index, TaskItem task)
        {
            Console.WriteLine($"{index+1}. {task.Name}");
        }
    }
}