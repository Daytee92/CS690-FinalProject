using System;
using System.Collections.Generic;

namespace TimeManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            List<Task> tasks = new List<Task>();  // List to store tasks
            TaskCreator taskCreator = new TaskCreator(tasks);  // CreateTask functionality
            TaskViewer taskViewer = new TaskViewer(tasks);    // ViewTask functionality

            bool running = true;

            while (running)
            {
                Console.Clear();
                Console.WriteLine("Welcome User! ");
                Console.WriteLine("Please choose an option:");
                Console.WriteLine("1. Create New Task");
                Console.WriteLine("2. Display Tasks");
                Console.WriteLine("3. View Productivity Summary: ");
                Console.WriteLine("4. Exit");
                Console.Write("\nPlease enter your choice (1-4): ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        taskCreator.CreateTask();  // Call CreateTask to add a new task
                        break;
                    case "2":
                        taskViewer.ViewTasks();   // Call ViewTasks to view all tasks
                        break;
                    case "3":
                        Console.WriteLine("Still in production..");
                        break;
                    case "4":
                        running = false;  // Exit the program
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }

                // Wait for user to press a key before returning to the menu
                Console.WriteLine("\nPress any key to return to the menu...");
                Console.ReadKey();
            }
        }
    }
}
