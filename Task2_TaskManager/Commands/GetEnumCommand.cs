using System.Text;

namespace Task2_TaskManager.Commands
{
    public class GetEnumCommand<T> : WaitingInputCommand where T : Enum
    {
        public int EnumIndex => _enumIndex;

        private readonly Type _enumType;

        private int _enumIndex;

        public GetEnumCommand()
        {
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
                _enumIndex = num;
                _isCorrectLine = true;
            }
            else
            {
                Console.WriteLine($"Incorrect {_enumType} number!");
            }
        }
    }
}