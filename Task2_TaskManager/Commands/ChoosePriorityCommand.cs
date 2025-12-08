using Task2_TaskManager.Enums;

namespace Task2_TaskManager.Commands
{
    public class ChoosePriorityCommand : AppCommand
    {
        private ICommand _exitCommand;

        public ChoosePriorityCommand(ICommand exitCommand)
        {
            _name = "Enter priority";

            _exitCommand = exitCommand;
        }

        public override void Execute()
        {
            base.Execute();

            var inputLine = "";

            var correctPr = false;

            while (!correctPr)
            {
                inputLine = Console.ReadLine();

                if (inputLine == "q")
                {
                    _exitCommand.Execute();
                }

                if (int.TryParse(inputLine, out var num) && Enum.IsDefined(typeof(Priority), --num))
                {
                    Console.WriteLine($"priority is {(Priority)num}");
                    correctPr = true;
                }
                else
                {
                    Console.WriteLine("Incorrect priority, enter other number");
                }
            }
        }
    }
}