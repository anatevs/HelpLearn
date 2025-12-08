// See https://aka.ms/new-console-template for more information
using Task2_TaskManager.Commands;
using Task2_TaskManager.Enums;

Console.WriteLine("Hello, World!");


var isActive = true;

while (isActive)
{
    Console.WriteLine("Create new task");

    var exitCm = new ExitGameCommand();
    var chsP = new ChoosePriorityCommand(exitCm);

    chsP.Execute();
}

return;