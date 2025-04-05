using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;

namespace TimeManagement
{
    public class TaskManager
    {
        private const string FilePath = "tasks.json";  // Path to store tasks

        // Save tasks to a JSON file
        public static void SaveTasks(List<Task> tasks)
        {
            string json = JsonConvert.SerializeObject(tasks, Formatting.Indented);
            File.WriteAllText(FilePath, json);
        }

        // Load tasks from the JSON file
        public static List<Task> LoadTasks()
        {
            if (File.Exists(FilePath))
            {
                string json = File.ReadAllText(FilePath);
                return JsonConvert.DeserializeObject<List<Task>>(json);
            }
            else
            {
                return new List<Task>();  // Return an empty list if no tasks file exists
            }
        }
    }
}
