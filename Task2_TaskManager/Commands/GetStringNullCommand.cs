namespace Task2_TaskManager.Commands
{
    public class GetStringNullCommand : WaitingInputCommand
    {
        public string? Result => _result;

        private string? _result = null;

        public GetStringNullCommand(string commandName)
        {
            _name = commandName;
        }

        protected override void HandleInput(string? input)
        {
            _result = input;
            _isCorrectLine = true;
        }
    }
}