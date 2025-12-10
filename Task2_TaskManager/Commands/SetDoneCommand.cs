using Task2_TaskManager.TaskItems;

namespace Task2_TaskManager.Commands
{
    public class SetDoneCommand : WaitingTaskNumberCommand
    {
        public SetDoneCommand(TasksList tasksList) : base(tasksList)
        {
        }

        protected override void HandleInput(string? input)
        {
            if (int.TryParse(input, out var number) && _tasksList.TrySetDone(number))
            {
                _isCorrectLine = true;
            }
            else
            {
                Console.WriteLine("Incorrect task number");
            }
        }
    }
}