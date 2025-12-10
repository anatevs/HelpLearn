using Task2_TaskManager.TaskItems;

namespace Task2_TaskManager.Commands
{
    public class SetDoneCommand : WaitingTaskNumberCommand
    {
        public SetDoneCommand(TasksList tasksList) : base(tasksList)
        {
            _name = "Set task as done";
        }

        protected override void HandleNumber(int number)
        {
            if (_tasksList.TrySetDone(number))
            {
                _isCorrectLine = true;
            }
        }
    }
}