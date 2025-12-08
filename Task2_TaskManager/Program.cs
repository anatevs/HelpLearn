// See https://aka.ms/new-console-template for more information
using Task2_TaskManager.Commands;
using Task2_TaskManager.Enums;
using Task2_TaskManager.TaskItems;

Console.WriteLine("Hello, World!");


var isActive = true;

//while (isActive)
//{
//    Console.WriteLine("Create new task");

//    var exitCm = new ExitGameCommand();
//    var chsP = new SortPriorityCommand();

//    chsP.Execute();


//    Console.WriteLine("press q for exit or other to proceed");
//    var key = Console.ReadLine();
//    if (key == "q")
//    {
//        exitCm.Execute();
//    }
//}

var tasks = new TasksList();

var tArray = new TaskItem[7];

tArray[0] = new TaskItem("t1", "", Priority.High, Category.Home, Status.New);
tArray[1] = new TaskItem("t2", "", Priority.Medium, Category.Home, Status.InProgress);
tArray[2] = new TaskItem("t3", "", Priority.Low, Category.Home, Status.New);
tArray[3] = new TaskItem("t4", "", Priority.Low, Category.Home, Status.Done);
tArray[4] = new TaskItem("t5", "", Priority.High, Category.Home, Status.Done);
tArray[5] = new TaskItem("t6", "", Priority.Medium, Category.Home, Status.InProgress);
tArray[6] = new TaskItem("t7", "", Priority.Medium, Category.Home, Status.New);

tasks.AddTasks(tArray);

tasks.FilterStatus(Status.New);
tasks.FilterStatus(Status.InProgress);
tasks.FilterStatus(Status.Done);

var sortCm = new SortPriorityCommand(tasks);
sortCm.Execute();

return;