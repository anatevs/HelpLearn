using Task2_TaskManager.TaskItems;

namespace Task2_TaskManager.Commands
{
    public class RemoveTaskCommand : WaitingTaskNumberCommand
    {
        public RemoveTaskCommand(TasksList tasksList) : base(tasksList)
        {
            _name = "Remove task from list";
        }

        protected override void HandleNumber(int number)
        {
            if (_tasksList.TryRemoveTask(number, out var removedName))
            {
                _isCorrectLine = true;
                Console.WriteLine($"Task {removedName} has been removed");
            }
        }
    }
}