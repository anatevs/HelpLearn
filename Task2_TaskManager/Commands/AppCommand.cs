namespace Task2_TaskManager.Commands
{
    public class AppCommand : ICommand
    {
        public string Name => _name;

        protected string _name = "";

        public virtual void Execute()
        {
            Console.WriteLine($"{_name}");
        }
    }
}