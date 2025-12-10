using System.Text.Json;
using Task2_TaskManager.TaskItems;

namespace Task2_TaskManager.Inits
{
    public class SaveLoadInit
    {
        private readonly TasksList _tasksList;

        private readonly string _filename = "SavedTasks.json";

        public SaveLoadInit(TasksList tasksList)
        {
            _tasksList = tasksList;
        }

        public void Save()
        {
            var data = _tasksList.GetData();

            if (data.Length != 0)
            {
                var jsonString = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });

                File.WriteAllText(_filename, jsonString);
            }
        }

        public void Load()
        {
            if (File.Exists(_filename))
            {
                var jsonString = File.ReadAllText(_filename);

                var data = JsonSerializer.Deserialize<TaskParams[]>(jsonString);

                _tasksList.SetData(data);

                Console.WriteLine("\nTasks loaded from file");
            }
            else
            {
                Console.WriteLine("No saved tasks");
            }
        }
    }
}