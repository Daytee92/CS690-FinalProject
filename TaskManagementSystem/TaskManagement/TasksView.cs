using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace TaskManagement
{
    public class TaskViewer
    {
        private List<Task> _tasks;

        public TaskViewer(List<Task> tasks)
        {
            _tasks = tasks;
        }

        // Method to view tasks and select options for editing, deleting, or starting a timer
        public void ViewTasks()
        {
            Console.Clear();
            Console.WriteLine("All Tasks\n");

            if (_tasks.Count == 0)
            {
                Console.WriteLine("No tasks available.");
            }
            else
            {
                // Display all tasks with their index
                for (int i = 0; i < _tasks.Count; i++)
                {
                    // Display task number and call DisplayTask for detailed task info
                    Console.WriteLine($"{i + 1}.");  // Display the task number (i+1 because list is 0-indexed)
                    _tasks[i].DisplayTask();  // Call the DisplayTask method to show formatted task details
                    Console.WriteLine();
                }
            }

            // Prompt the user to select a task
            Console.WriteLine("\nSelect a task number to interact with or press Enter to return to the main menu.");
            string input = Console.ReadLine();

            if (int.TryParse(input, out int taskNumber) && taskNumber >= 1 && taskNumber <= _tasks.Count)
            {
                Task selectedTask = _tasks[taskNumber - 1];

                // Display options for the selected task
                Console.Clear();
                Console.WriteLine($"You have selected: {selectedTask.Name}");
                Console.WriteLine("Options:");
                Console.WriteLine("1. Edit Task");
                Console.WriteLine("2. Delete Task");
                Console.WriteLine("3. Start Timer");
                Console.WriteLine("4. Return to Main Menu");

                Console.Write("\nSelect an option (1-4): ");
                string actionChoice = Console.ReadLine();

                switch (actionChoice)
                {
                    case "1":
                        EditTask(selectedTask);
                        break;
                    case "2":
                        DeleteTask(selectedTask);
                        break;
                    case "3":
                        StartTimer(selectedTask);
                        break;
                    case "4":
                        break;  // Return to main menu
                    default:
                        Console.WriteLine("Invalid option. Returning to task list.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid task number. Returning to the main menu...");
            }
        }

        // Edit task method
        public void EditTask(Task task)
        {
            Console.Clear();
            Console.WriteLine("Edit Task\n");

            // Edit task name
            Console.Write($"Current Task Name: {task.Name}\nEnter new name (or press Enter to keep current): ");
            string newName = Console.ReadLine();
            if (!string.IsNullOrEmpty(newName)) task.Name = newName;

            // Edit priority
            Console.Write($"Current Priority: {task.Priority}\nEnter new priority (High, Medium, Low): ");
            string newPriority = Console.ReadLine().ToLower();
            if (newPriority == "high" || newPriority == "medium" || newPriority == "low") 
                task.Priority = newPriority.ToUpper();

            // Edit due date
            Console.Write($"Current Due Date: {task.DueDate.ToShortDateString()}\nEnter new due date (YYYY-MM-DD): ");
            string dueDateInput = Console.ReadLine();
            if (DateTime.TryParse(dueDateInput, out DateTime newDueDate))
                task.DueDate = newDueDate;

            // Mark as complete
            Console.Write("Mark as complete? (Y/N): ");
            string markComplete = Console.ReadLine().ToLower();
            if (markComplete == "y") task.IsComplete = true;

            Console.WriteLine("\nTask updated successfully!");
        }

        // Delete task method
        public void DeleteTask(Task task)
        {
            Console.Clear();
            Console.WriteLine($"Are you sure you want to delete the task: {task.Name}? (Y/N)");
            string confirmation = Console.ReadLine().ToLower();

            if (confirmation == "y")
            {
                _tasks.Remove(task);
                Console.WriteLine("Task deleted successfully.");
            }
            else
            {
                Console.WriteLine("Task not deleted.");
            }
        }

        // Start timer method
        public void StartTimer(Task task)
        {
            Console.Clear();
            Console.WriteLine($"Starting timer for task: {task.Name}");
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            // Inform the user that the timer is running and they can stop it with Enter
            Console.WriteLine("Press Enter to stop the timer.");
            Console.ReadLine();  // Wait for the user to press Enter

            stopwatch.Stop();
            TimeSpan timeSpent = stopwatch.Elapsed;
            Console.WriteLine($"Total time spent on task: {timeSpent}");
        }
    }
}
