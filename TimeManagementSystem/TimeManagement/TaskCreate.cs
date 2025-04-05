using System;
using System.Collections.Generic;

namespace TimeManagement
{
    public class TaskCreator
    {
        private List<Task> tasks; // List to store tasks

        // Initialize the tasks list as empty
        public TaskCreator(List<Task> tasks)
        {
            this.tasks = tasks;  // Accepts a list of tasks
        }

        // Create a new task
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

            // Ask the user if they want to set a reminder
            DateTime? reminder = null;
            while (true)
            {
                Console.Write("Do you want to set a reminder for this task? (yes/no): ");
                string reminderChoice = Console.ReadLine().ToLower();
                if (reminderChoice == "yes")
                {
                    DateTime reminderDate;
                    while (true)
                    {
                        Console.Write("Enter reminder date and time (MM/dd/yyyy HH:mm): ");
                        if (DateTime.TryParse(Console.ReadLine(), out reminderDate))
                        {
                            reminder = reminderDate;
                            break;
                        }
                        else
                            Console.WriteLine("Invalid date and time format. Please try again.");
                    }
                    break;
                }
                else if (reminderChoice == "no")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please enter 'yes' or 'no'.");
                }
            }
           
            // Create the task and add to the list
            tasks.Add(new Task(taskName, priority.ToUpper(), dueDate, category.ToUpper(), reminder));

            Console.WriteLine("\nTask Created Successfully!\n");
        }
    }
}
