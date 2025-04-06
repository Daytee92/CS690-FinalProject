using System;
using System.Collections.Generic;

namespace TaskManagement
{
    public class TaskViewer
    {
        private List<Task> tasks;

        public TaskViewer(List<Task> tasks)
        {
            this.tasks = tasks;
        }

        public void ViewTasks()
        {
            Console.Clear();
            Console.WriteLine("All Tasks\n");

            if (tasks.Count == 0)
            {
                Console.WriteLine("No tasks available.");
            }
            else
            {
                for (int i = 0; i < tasks.Count; i++)
                {
                    Console.WriteLine($"{i + 1}."); 
                    tasks[i].DisplayTask();  
                    Console.WriteLine();  
                }
            }

            // Ask the user to select a task or press Enter to return to the main menu
            Console.WriteLine("\nSelect a task number to interact with or press Enter to return to the main menu.");
            string input = Console.ReadLine();

            if (int.TryParse(input, out int taskNumber) && taskNumber >= 1 && taskNumber <= tasks.Count)
            {
                Task selectedTask = tasks[taskNumber - 1]; 

                // Present options for task interaction
                Console.Clear();
                Console.WriteLine($"You have selected: {selectedTask.Name}");
                Console.WriteLine("Options:");
                Console.WriteLine("1. Edit Task");
                Console.WriteLine("2. Delete Task");
                Console.WriteLine("3. Start Task");
                Console.WriteLine("4. Return to Main Menu");

                Console.Write("\nSelect an option (1-4): ");
                string actionChoice = Console.ReadLine();

                switch (actionChoice)
                {
                    case "1":
                        Console.WriteLine("Edit Task in production");  // Edit the selected task
                        break;
                    case "2":
                        Console.WriteLine("Delete Task in production");;  // Delete the selected task
                        break;
                    case "3":
                        Console.WriteLine("Start Task in production");;  // Start a timer for the selected task
                        break;
                    case "4":
                        break;  // Return to main menu
                    default:
                        Console.WriteLine("Invalid Option. Returning to task list.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("No task selected. Returning to the main menu...");
            }
        }
    }
}
