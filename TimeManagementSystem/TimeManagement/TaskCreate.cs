using System;
using System.Collections.Generic;

namespace TimeManagement
{
    public class TaskCreator
    {
        private List<Task> tasks; // List to store tasks

        // Constructor to initialize the tasks list as empty
        public TaskCreator(List<Task> tasks)
        {
            this.tasks = tasks;  // Accepts a list of tasks from the calling code
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

            Console.WriteLine("\nTask Created Successfully!\n");
        }
    }
}
