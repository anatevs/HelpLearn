namespace Task1_Items.Commands
{
    public interface ICommand
    {
        public string Name { get; }

        public void Execute();
    }
}