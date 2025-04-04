using System;
using System.Collections.Generic;

namespace TimeManagement
{
    public class TaskViewer
    {
        private List<Task> tasks;

        // Constructor to initialize the list of tasks
        public TaskViewer(List<Task> tasks)
        {
            this.tasks = tasks;  // Accept the task list to display tasks
        }

        // Method to view all tasks
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
                // Display all tasks
                for (int i = 0; i < tasks.Count; i++)
                {
                    tasks[i].DisplayTask();  // Display each task
                }
            }

            // Ask the user to select a task to interact with
            Console.WriteLine("\nSelect a task number to interact with, or press Enter to return to the main menu.");
            string input = Console.ReadLine();

            if (int.TryParse(input, out int taskNumber) && taskNumber >= 1 && taskNumber <= tasks.Count)
            {
                Task selectedTask = tasks[taskNumber - 1];  // Get the selected task

                // Display options for the selected task
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
                        Console.WriteLine("Edit Task functionality is not implemented yet.");
                        break;
                    case "2":
                        Console.WriteLine("Delete Task functionality is not implemented yet.");
                        break;
                    case "3":
                        Console.WriteLine("Start Task functionality is not implemented yet.");
                        break;
                    case "4":
                        break; // Return to main menu
                    default:
                        Console.WriteLine("Invalid option. Returning to task list.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Returning to the main menu...");
            }
        }
    }
}
