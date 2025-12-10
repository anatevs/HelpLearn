using System.Text;
using Task2_TaskManager.TaskItems;

namespace Task2_TaskManager.Commands
{
    public abstract class WaitingTaskNumberCommand : WaitingInputCommand
    {
        protected TasksList _tasksList;

        public WaitingTaskNumberCommand(TasksList tasksList)
        {
            _tasksList = tasksList;
        }

        public override void Execute()
        {
            PrepareExecute();

            base.Execute();
        }

        protected override abstract void HandleInput(string? input);

        protected virtual void PrepareExecute()
        {
            var title = new StringBuilder($"Enter task number:");
            var names = _tasksList.GetNames();

            for (int i = 0; i < names.Length; i++)
            {
                title.Append($"\n{i + 1} - {names[i]}");
            }

            _name = title.ToString();
        }
    }
}
