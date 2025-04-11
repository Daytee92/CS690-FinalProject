using System;
using System.Collections.Generic;

namespace TaskManagement
{
    public class TaskCreator
    {
        private List<Task> _tasks;

        public TaskCreator(List<Task> tasks)
        {
            _tasks = tasks;
        }

        // Method to create a task based on user input (no parameters)
        public void CreateTask()
        {
            Console.Clear();
            Console.WriteLine("Create New Task");

            // Ask for task name
            string taskName = "";
            while (string.IsNullOrWhiteSpace(taskName)){
                Console.Write("Enter task name: ");
                taskName = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(taskName))
                {
                    Console.WriteLine("Task name cannot be empty. Please enter a valid task name.");
                }

            }
            
            // Ask for priority
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

            // Ask for due date
            DateTime dueDate;
            while (true)
            {
                Console.Write("Enter task due date (MM/dd/yyyy): ");
                if (DateTime.TryParse(Console.ReadLine(), out dueDate))
                {
                    if (dueDate.Date < DateTime.Today)
                    {
                        Console.WriteLine("Date cannot be in the past, please try again");
                    }
                    else
                    {
                        break;
                    }
                    
                }
                    
                else
                    Console.WriteLine("Invalid date format. Please try again.");
            }

            // Ask for category
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

            // Ask for reminder
            DateTime? reminder = null;
            while (true)
            {
                Console.Write("Do you want to set a reminder for this task? (yes or no): ");
                string reminderChoice = Console.ReadLine().ToLower();
                if (reminderChoice == "yes")
                {
                    DateTime reminderDate;
                    while (true)
                    {
                        Console.Write("Enter reminder date and time (MM/dd/yyyy HH:mm): ");
                        if (DateTime.TryParse(Console.ReadLine(), out reminderDate))
                        {
                            if (reminderDate.Date < DateTime.Today)
                            {
                                Console.WriteLine("Date cannot be in the past");
                            }
                            else
                            {
                                reminder = reminderDate;
                                break;
                            }
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
                    Console.WriteLine("Invalid choice. Please enter yes or no.");
                }
            }

            // Create and add the task
            var task = new Task(taskName, priority.ToUpper(), dueDate, category.ToUpper(), reminder);
            _tasks.Add(task);

            Console.WriteLine("\nTask Created Successfully!");
        }

        // For testing)
        public void CreateTask(string name, string priority, DateTime dueDate, string category, DateTime? reminder = null)
        {
            var task = new Task(name, priority, dueDate, category, reminder);
            _tasks.Add(task);
        }
    }
}