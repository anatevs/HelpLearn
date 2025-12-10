using System.Xml.Linq;
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

        public TaskParams[] GetData()
        {
            var data = new TaskParams[_tasks.Count];

            for (int i = 0; i < data.Length; i++)
            {
                data[i] = new TaskParams(_tasks[i]);
            }

            return data;
        }

        public void SetData(TaskParams[]? tasksParams)
        {
            if (tasksParams != null)
            {
                foreach (var taskParam in tasksParams)
                {
                    _tasks.Add(new TaskItem(taskParam));
                }
            }
        }

        public bool TryRemoveTask(int number, out string name)
        {
            var result = CheckNumber(number);
            name = "";
            var index = number - 1;

            if (result)
            {
                name = _tasks[index].Name;
                _tasks.RemoveAt(index);
            }

            return result;
        }

        public bool TrySetDone(int number)
        {
            var result = CheckNumber(number);

            if (result)
            {
                _tasks[number - 1].SetDone();
            }

            return result;
        }

        private bool TryMakeAtNumber(int number, Action action)
        {
            var result = CheckNumber(number);

            if (result)
            {
                action.Invoke();
            }

            return result;
        }

        public void ShowList()
        {
            for (int i = 0; i < _tasks.Count; i++)
            {
                ShowTask(i + 1, _tasks[i]);
            }
        }

        public string[] GetNames()
        {
            IEnumerable<string> result =
                _tasks
                .Select(x => x.Name);

            return result.ToArray();
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

        public void ShowAll()
        {
            for (int i = 0; i < _tasks.Count; i++)
            {
                ShowTask(i, _tasks[i]);
            }
        }

        private bool CheckNumber(int number)
        {
            return (number > 0 && number <= _tasks.Count);
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
            Console.WriteLine($"{index+1}. {task.GetStringInfo()}");
        }
    }
}