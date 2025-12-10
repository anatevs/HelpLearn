namespace Task2_TaskManager.TaskItems
{
    public class TasksList
    {
        public IReadOnlyList<TaskItem> Tasks => _tasks;

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

        public bool TryRemoveTask(int number)
        {
            void Remove(int index)
            {
                var name = _tasks[index].Name;
                _tasks.RemoveAt(index);

                Console.WriteLine($"Task {name} removed");
            }

            return TryMakeAtNumber(number, Remove);
        }

        public bool TrySetDone(int number)
        {
            void SetDone(int index)
            {
                _tasks[index].SetDone();
                Console.WriteLine($"Task {_tasks[index].Name} marked as Done");
            }

            return TryMakeAtNumber(number, SetDone);
        }

        private bool TryMakeAtNumber(int number, Action<int> action)
        {
            var result = CheckNumber(number);
            var index = number - 1;

            if (result)
            {
                action.Invoke(index);
            }

            return result;
        }

        public void ShowAll()
        {
            for (int i = 0; i < _tasks.Count; i++)
            {
                TasksView.ShowTask(i, _tasks[i]);
            }
        }

        private bool CheckNumber(int number)
        {
            return (number > 0 && number <= _tasks.Count);
        }
    }
}