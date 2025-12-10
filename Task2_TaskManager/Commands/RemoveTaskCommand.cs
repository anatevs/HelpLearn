using Task2_TaskManager.TaskItems;

namespace Task2_TaskManager.Commands
{
    public class RemoveTaskCommand : WaitingTaskNumberCommand
    {
        public RemoveTaskCommand(TasksList tasksList) : base(tasksList)
        {
        }

        protected override void HandleNumber(int number)
        {
            if (_tasksList.TryRemoveTask(number))
            {
                _isCorrectLine = true;
            }
        }
    }
}