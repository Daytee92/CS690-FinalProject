using System;
using System.Collections.Generic;
namespace TimeManagement;

class Program
{
    static void Main(string[] args)
    {
        List<Task> tasks = new List<Task>(); // Store tasks

        while (true)
        {
            Console.WriteLine("Welcome User!");
            Console.WriteLine("Please choose an option:");
            Console.WriteLine("1. Create New Task");
            Console.WriteLine("2. Display all Tasks");
            Console.WriteLine("3. View Productivity Report");
            Console.WriteLine("4. Exit");
            Console.Write("\nEnter your choice (1-4): ");
            
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CreateTask(tasks);
                    break;
                case "2":
                    ViewTasks(tasks);
                    break;
                case "3":
                    ViewProductivityReport(tasks);
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }

    // Method to create a task
    static void CreateTask(List<Task> tasks)
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
    static void ViewTasks(List<Task> tasks)
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
                var task = tasks[i];
                Console.WriteLine($"{i + 1}. Task: {task.Name} | Priority: {task.Priority} | Due Date: {task.DueDate.ToShortDateString()} | Category: {task.Category}");
            }

            // Ask the user for interaction after task display
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
                        // EditTask(selectedTask); // Call the edit task logic here
                        break;
                    case "2":
                        // DeleteTask(tasks, taskNumber - 1); // Call the delete task logic here
                        break;
                    case "3":
                        // StartTimer(selectedTask); // Call the start timer logic here
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

        // Automatically go back to the main menu after displaying tasks
    }

    // Method to view productivity report
    static void ViewProductivityReport(List<Task> tasks)
    {
        Console.Clear();
        Console.WriteLine("Productivity Report\n");

        int completedTasks = 0;
        int overdueTasks = 0;

        foreach (var task in tasks)
        {
            // For simplicity, let's assume tasks are "completed" if their due date has passed
            if (task.DueDate < DateTime.Now)
            {
                overdueTasks++;
            }
        }

        completedTasks = tasks.Count - overdueTasks;

        // Displaying basic productivity data
        Console.WriteLine($"Total Tasks: {tasks.Count}");
        Console.WriteLine($"Completed Tasks: {completedTasks}");
        Console.WriteLine($"Overdue Tasks: {overdueTasks}");
        // In a real-world scenario, you would sum up the tracked times for each task to get the total time spent

        // Automatically go back to main menu after displaying report
    }
}

// Task class to hold task data
public class Task
{
    public string Name { get; set; }
    public string Priority { get; set; }
    public DateTime DueDate { get; set; }
    public string Category { get; set; }  // New property for category

    public Task(string name, string priority, DateTime dueDate, string category)
    {
        Name = name;
        Priority = priority;
        DueDate = dueDate;
        Category = category;
    }
}
