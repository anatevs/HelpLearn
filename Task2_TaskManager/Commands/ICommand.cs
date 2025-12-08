namespace Task2_TaskManager.Commands
{
    public interface ICommand
    {
        public string Name { get; }

        public void Execute();
    }
}