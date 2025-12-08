using System.Text;
using Task2_TaskManager.Commands;
using Task2_TaskManager.Enums;

namespace Task2_TaskManager.TaskItems
{
    public class FilterCategoryCommand : WaitingInputCommand
    {
        public FilterCategoryCommand()
        {
            var title = new StringBuilder("Enter priority number:");

            foreach (var category in Enum.GetValues(typeof(Category)))
            {
                title.Append($"\n{(int)category + 1} - {category}");
            }

            _name = title.ToString();
        }

        protected override void HandleInput(string? inputLine)
        {
            if (int.TryParse(inputLine, out var num) && Enum.IsDefined(typeof(Category), --num))
            {
                Console.WriteLine($"category is {(Category)num}");
                _isCorrectLine = true;
            }
            else
            {
                Console.WriteLine("Incorrect category number!");
            }
        }
    }
}