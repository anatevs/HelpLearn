// See https://aka.ms/new-console-template for more information
using Task2_TaskManager.Inits;
using Task2_TaskManager.TaskItems;

var tasks = new TasksList();

//tasks = TestTaskList.GetTasks();

var mainMenu = new MainMenuInit(tasks);
var mainCommands = mainMenu.Init();

var isActive = true;

while (isActive)
{
    mainCommands.ShowOptions();

    var key = Console.ReadLine();

    if (key != null)
    {
        mainCommands.InvokeOption(key);
    }
    else
    {
        Console.WriteLine("null key entered");
    }
}

return;