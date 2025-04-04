using System;
namespace TimeManagement;
class Program
{
    static void Main(string[] args)
    {
        TaskManager taskManager = new TaskManager(); // Create TaskManager instance

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Welcome to Task Management System");
            Console.WriteLine("Please choose an option:");
            Console.WriteLine("1. Create New Task");
            Console.WriteLine("2. View All Tasks");
            Console.WriteLine("3. View Productivity Report");
            Console.WriteLine("4. Exit");
            Console.Write("\nEnter your choice (1-4): ");
            
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    taskManager.CreateTask();  // Create new task
                    break;
                case "2":
                    taskManager.ViewTasks();    // View all tasks and interact
                    break;
                case "3":
                    taskManager.ViewProductivityReport();  // View productivity report
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }
}
