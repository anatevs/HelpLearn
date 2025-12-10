using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2_TaskManager.Enums;

namespace Task2_TaskManager.TaskItems
{
    public class TestTaskList
    {
        public static TasksList GetTasks()
        {
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

            return tasks;
        }
    }
}