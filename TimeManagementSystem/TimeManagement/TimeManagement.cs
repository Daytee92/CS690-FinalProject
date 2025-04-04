using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace TimeManagement;

public class TaskManager
{
    private List<Task> tasks;

    public TaskManager()
    {
        tasks = new List<Task>();
    }

    // Method to create a new task
    public void CreateTask()
    {
         Console.Clear();
        Console.WriteLine("Create New Task");

        Console.Write("Enter task name: ");
        string taskName = Console.ReadLine();

        string priority = "";
        while (true)
        {
            Console.Write("Enter task priority (High, Medium, Low): ");
            priority = Console.ReadLine().ToLower();
            if (priority == "high" || priority == "medium" || priority == "low")
                break;
            else
                Console.WriteLine("Invalid priority. Please enter High, Medium, or Low.");
        }

        DateTime dueDate;
        while (true)
        {
            Console.Write("Enter task due date (MM/dd/yyyy): ");
            if (DateTime.TryParse(Console.ReadLine(), out dueDate))
                break;
            else
                Console.WriteLine("Invalid date format. Please try again.");
        }

        string category = "";
        while (true)
        {
            Console.Write("Enter task category (Personal, Work): ");
            category = Console.ReadLine().ToLower();
            if (category == "personal" || category == "work")
                break;
            else
                Console.WriteLine("Invalid category. Please enter Personal or Work.");
        }

        // Create the task and add to the list
        tasks.Add(new Task(taskName, priority.ToUpper(), dueDate, category.ToUpper()));

        Console.Clear(); // Clear screen to show task list right away
        Console.WriteLine("\nTask Created Successfully!\n");
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
            for (int i = 0; i < tasks.Count; i++)
            {
                tasks[i].DisplayTask();
            }

            // Ask the user to select a task
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

    // Method to edit a task
    private void EditTask(Task task)
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
        Console.Write($"Current Due Date: {task.DueDate.ToShortDateString()}\nEnter new due date (MM/dd/yyyy): ");
        if (DateTime.TryParse(Console.ReadLine(), out DateTime newDueDate))
            task.DueDate = newDueDate;

        // Mark as complete
        Console.Write("Mark as complete? (Y/N): ");
        string markComplete = Console.ReadLine().ToLower();
        if (markComplete == "y") task.IsComplete = true;

        Console.WriteLine("\nTask updated successfully!");
    }

    // Method to delete a task with confirmation
    private void DeleteTask(Task task)
    {
        Console.Clear();
        Console.WriteLine($"Are you sure you want to delete the task: {task.Name}? (Y/N)");
        string confirmation = Console.ReadLine().ToLower();

        if (confirmation == "y")
        {
            tasks.Remove(task);
            Console.WriteLine("Task deleted successfully.");
        }
        else
        {
            Console.WriteLine("Task not deleted.");
        }
    }

    // Method to start a timer for a task
    private void StartTimer(Task task)
    {
        Console.Clear();
        Console.WriteLine($"Timer started for task: {task.Name}");
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        Console.WriteLine("Press Enter to stop the timer.");
        Console.ReadLine();  // Wait for the user to press Enter

        stopwatch.Stop();
        TimeSpan timeSpent = stopwatch.Elapsed;
        Console.WriteLine($"Total time spent on task: {timeSpent}");

        // Optionally, you can save the time spent on the task if needed
    }

    // Method to view productivity report
    public void ViewProductivityReport()
    {
        Console.Clear();
        Console.WriteLine("Productivity Report\n");

        int totalTasks = tasks.Count;
        int completedTasks = 0;
        int overdueTasks = 0;
        TimeSpan totalTimeSpent = TimeSpan.Zero; // You could track this if you implement time tracking.

        foreach (var task in tasks)
        {
            if (task.IsComplete) completedTasks++;
            if (task.DueDate < DateTime.Now && !task.IsComplete) overdueTasks++;

            // Optional: if you track time spent per task, you could add it up here
            // For example: totalTimeSpent += task.TimeSpent;
        }
    }
}
