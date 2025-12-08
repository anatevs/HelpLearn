namespace Task2_TaskManager.Commands
{
    public abstract class WaitingInputCommand : BaseCommand
    {
        protected bool _isCorrectLine = false;

        public override void Execute()
        {
            base.Execute();

            _isCorrectLine = false;

            while (!_isCorrectLine)
            {
                var inputLine = Console.ReadLine();

                HandleInput(inputLine);
            }
        }

        protected abstract void HandleInput(string? input);
    }
}