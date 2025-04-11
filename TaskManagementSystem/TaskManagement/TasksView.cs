using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Formats.Asn1;

namespace TaskManagement
{
    public class TaskViewer
    {
        private List<Task> _tasks;

        public TaskViewer(List<Task> tasks)
        {
            _tasks = tasks;
        }

        // Edit a task
        public void EditTask(Task task)
        {
            // Edit task name
            Console.WriteLine("Enter new name or press Enter to keep current name: ");
            string newName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(newName)) newName = task.Name;

            // Edit priority with validation
            Console.WriteLine("Enter new priority or press eneter to keep current priority: ");
            string newPriority = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(newPriority)) newPriority = task.Priority;

            // Validate priority input

            if (newPriority.ToLower() != "high" && newPriority.ToLower() != "medium" && newPriority.ToLower() != "low")
            {
                newPriority = task.Priority;  // If input is invalid, keep current priority
                Console.WriteLine("[bold red]Invalid priority. Keeping the current priority.[/]");
            }

            // Edit due date with validation
            Console.WriteLine("Enter new date or press enter to keep current due date: ");
            string dueDateInput = Console.ReadLine();
            DateTime newDueDate = task.DueDate; // Default to current due date

            if (!string.IsNullOrWhiteSpace(dueDateInput)) // Only try to parse if user enters something
            {
                if (DateTime.TryParse(dueDateInput, out newDueDate))
                {
                    // If parsing is successful, update due date
                }
                else
                {
                    Console.WriteLine("Invalid date format. Keeping the current due date.[/]");
                }
            }

            bool markComplete = task.IsComplete;
            string complete = "";
            Console.WriteLine($"Current Status: {(task.IsComplete ? "Completed" : "Incomplete")}");
            Console.WriteLine("Mark Task as complete? (Yes/No)");
            complete = Console.ReadLine().ToLower();

            if (complete == "yes")
                task.IsComplete = true;
            else if (complete == "no")
                task.IsComplete = false;
            else {
                Console.WriteLine("Invalid choice. Exiting");
            }

            // Update task properties
            task.Name = newName;
            task.Priority = newPriority;
            task.DueDate = newDueDate;


            Console.WriteLine("Task updated successfully!");
        }

        // Delete a task
        public void DeleteTask(Task task)
        {
            Console.WriteLine("Are you sure you want to delete the selected task (Yes/No)?");
            string delete = Console.ReadLine().ToLower();
            if (delete == "yes")
            {
                _tasks.Remove(task);
                Console.WriteLine("Task Deleted Succesfully");
            }
            else if (delete =="no")
            {
                Console.WriteLine("Task Not Deleted");
            }
                
            else
            {
                Console.WriteLine("Invalid Entry");
            }
        }

        // Start timer for a task
        public TimeSpan StartTimer(Task task)
        {
            Console.WriteLine("Start Timer Now? (Yes/No)");
            string response = Console.ReadLine().ToLower();

            if (response == "yes")
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();

                Console.WriteLine("Press Enter to stop the timer.");
                Console.ReadLine();  // Wait for the user to press Enter

                stopwatch.Stop();
                Console.WriteLine($"Time elapsed: {stopwatch.Elapsed}");

        
                task.TimeSpent += stopwatch.Elapsed;

                return stopwatch.Elapsed;
            }
            else if (response == "no")
            {
                Console.WriteLine("Timer not started.");
                return TimeSpan.Zero;
            }
            else
            {
                Console.WriteLine("Invalid entry. Timer not started.");
                return TimeSpan.Zero;
            }
        }

        
    } 
}
