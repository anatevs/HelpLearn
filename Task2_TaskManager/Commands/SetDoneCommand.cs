using System.Text;
using Task2_TaskManager.TaskItems;

namespace Task2_TaskManager.Commands
{
    public class SetDoneCommand : WaitingInputCommand
    {
        private TasksList _tasksList;

        public SetDoneCommand(TasksList tasksList)
        {
            _tasksList = tasksList;
        }

        protected override void HandleInput(string? input)
        {
            if (int.TryParse(input, out var number) && _tasksList.TrySetDone(number))
            {
                _isCorrectLine = true;
            }
            else
            {
                Console.WriteLine("Incorrect task number");
            }
        }

        protected override void PrepareExecute()
        {
            var title = new StringBuilder($"Enter task number:");
            var names = _tasksList.GetNames();

            for (int i = 0; i < names.Length; i++)
            {
                title.Append($"\n{i + 1} - {names[i]}");
            }

            _name = title.ToString();
        }
    }
}