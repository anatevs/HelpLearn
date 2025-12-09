// See https://aka.ms/new-console-template for more information
using Task2_TaskManager.Commands;
using Task2_TaskManager.Enums;
using Task2_TaskManager.TaskItems;

Console.WriteLine("Hello, World!");


var tasks = new TasksList();

var tArray = new TaskItem[7];

tArray[0] = new TaskItem("t1", "", Priority.High, Category.Work, Status.New);
tArray[1] = new TaskItem("t2", "", Priority.Medium, Category.Home, Status.InProgress);
tArray[2] = new TaskItem("t3", "", Priority.Low, Category.Study, Status.New);
tArray[3] = new TaskItem("t4", "", Priority.Low, Category.Other, Status.Done);
tArray[4] = new TaskItem("t5", "", Priority.High, Category.Home, Status.Done);
tArray[5] = new TaskItem("t6", "", Priority.Medium, Category.Study, Status.InProgress);
tArray[6] = new TaskItem("t7", "", Priority.Medium, Category.Work, Status.New);

tasks.AddTasks(tArray);

var isActive = true;

while (isActive)
{
    Console.WriteLine("Create new task");

    var exitCm = new ExitGameCommand();
    //var flCtCm = new FilterCommand<Category>(tasks);

    //flCtCm.Execute();

    //var flStCm = new FilterCommand<Status>(tasks);

    //flStCm.Execute();


    //var add = new AddTaskCommand(tasks);
    //add.Execute();

    var setDone = new SetDoneCommand(tasks);
    setDone.Execute();

    var show = new ShowTasksCommand(tasks);
    show.Execute();

    Console.WriteLine("press q for exit or other to proceed");
    var key = Console.ReadLine();
    if (key == "q")
    {
        exitCm.Execute();
    }
}

var sortCm = new SortPriorityCommand(tasks);
sortCm.Execute();

return;