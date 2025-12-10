using Task2_TaskManager.Enums;
using Task2_TaskManager.TaskItems;

namespace Task2_TaskManager.Commands
{
    public class ShowStatusStatistic : BaseTasksCommand
    {
        public ShowStatusStatistic(TasksList tasksList) : base(tasksList)
        {
            _name = "Show statistic by status";
            _executeText = "\nStatistic by status:";
        }

        public override void Execute()
        {
            base.Execute();

            foreach (Status status in Enum.GetValues(typeof(Status)))
            {
                var number = _tasksList.Tasks.Count(x => x.Status == status);

                Console.WriteLine($"{status} - {number}");
            }
        }
    }
}