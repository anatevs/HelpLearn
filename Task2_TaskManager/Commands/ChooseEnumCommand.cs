using System.Text;

namespace Task2_TaskManager.Commands
{
    public abstract class ChooseEnumCommand<T> : WaitingInputCommand where T : Enum
    {
        protected readonly Type _enumType;

        public ChooseEnumCommand()
        {
            _enumType = typeof(T);

            var title = new StringBuilder($"Enter {_enumType.Name.ToLower()} number:");

            foreach (var enumValue in Enum.GetValues(_enumType))
            {
                title.Append($"\n{(int)enumValue + 1} - {enumValue}");
            }

            _executeText = title.ToString();
        }

        protected override void HandleInput(string? inputLine)
        {
            if (int.TryParse(inputLine, out var num) && Enum.IsDefined(_enumType, --num))
            {
                HandleEnumIndex(num);
                _isCorrectLine = true;
            }
            else
            {
                Console.WriteLine($"Incorrect {_enumType} number!");
            }
        }

        protected abstract void HandleEnumIndex(int enumIndex);
    }
}