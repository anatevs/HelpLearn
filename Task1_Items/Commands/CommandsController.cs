namespace Task1_Items.Commands
{
    public class CommandsController
    {
        private Dictionary<string, ICommand> _commandOptions = new();

        public void AddOption(string key, ICommand command)
        {
            if (_commandOptions.ContainsKey(key))
            {
                Console.WriteLine($"option with key {key} also exist in options");
            }

            _commandOptions.Add(key, command);
        }

        public void ClearOptions()
        {
            _commandOptions = new Dictionary<string, ICommand>();
        }

        public bool InvokeOption(string key)
        {
            if (_commandOptions.TryGetValue(key, out var command))
            {
                command.Execute();
                return true;
            }

            return false;
        }

        public void ShowOptions()
        {
            Console.WriteLine();
            Console.WriteLine("Choose from options:");

            foreach (var commandOption in _commandOptions)
            {
                Console.WriteLine($"{commandOption.Key} - {commandOption.Value.Name}");
            }
        }
    }
}