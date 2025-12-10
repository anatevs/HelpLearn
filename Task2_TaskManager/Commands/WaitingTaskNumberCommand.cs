using System.Text;
using Task2_TaskManager.TaskItems;

namespace Task2_TaskManager.Commands
{
    public abstract class WaitingTaskNumberCommand : WaitingInputCommand
    {
        protected TasksList _tasksList;

        public WaitingTaskNumberCommand(TasksList tasksList)
        {
            _tasksList = tasksList;
        }

        public override void Execute()
        {
            PrepareExecute();

            base.Execute();
        }

        protected override void HandleInput(string? input)
        {
            if (int.TryParse(input, out var number))
            {
                HandleNumber(number);
            }
            else
            {
                Console.WriteLine("Incorrect task number");
            }
        }

        protected abstract void HandleNumber(int number);

        protected virtual void PrepareExecute()
        {
            var title = new StringBuilder($"Enter task number:");
            var names = _tasksList.Tasks
                .Select(x => x.Name)
                .ToArray();

            for (int i = 0; i < names.Length; i++)
            {
                title.Append($"\n{i + 1} - {names[i]}");
            }

            _executeText = title.ToString();
        }
    }
}