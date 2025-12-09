using System.Text;
using Task2_TaskManager.TaskItems;

namespace Task2_TaskManager.Commands
{
    public class FilterCommand<T> : WaitingInputCommand where T : Enum
    {
        private readonly TasksList _tasksList;

        private Type _enumType;

        public FilterCommand(TasksList tasksList)
        {
            _tasksList = tasksList;
            _enumType = typeof(T);

            var title = new StringBuilder($"Enter {_enumType.Name.ToLower()} number:");

            foreach (var enumValue in Enum.GetValues(_enumType))
            {
                title.Append($"\n{(int)enumValue + 1} - {enumValue}");
            }

            _name = title.ToString();
        }

        protected override void HandleInput(string? inputLine)
        {
            if (int.TryParse(inputLine, out var num) && Enum.IsDefined(_enumType, --num))
            {
                _tasksList.FilterByEnum(_enumType, num);
                _isCorrectLine = true;
            }
            else
            {
                Console.WriteLine($"Incorrect {_enumType} number!");
            }
        }
    }
}