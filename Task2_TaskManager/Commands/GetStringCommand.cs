using Task2_TaskManager.TaskItems;

namespace Task2_TaskManager.Commands
{
    public class GetStringCommand : WaitingInputCommand
    {
        public string Result => _result;

        private string _result = "";

        public GetStringCommand(string commandName)
        {
            _name = commandName;
        }

        protected override void HandleInput(string? input)
        {
            if (input == null)
            {
                Console.WriteLine("Nothing has been entered, write once more");
            }
            else
            {
                _result = input;
                _isCorrectLine = true;
            }
        }
    }
}