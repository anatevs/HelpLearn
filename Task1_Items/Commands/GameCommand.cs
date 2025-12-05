namespace Task1_Items.Commands
{
    public class GameCommand : ICommand
    {
        public string Name => _name;

        protected string _name = "";

        public virtual void Execute()
        {
            Console.WriteLine($"{_name}");
        }
    }
}