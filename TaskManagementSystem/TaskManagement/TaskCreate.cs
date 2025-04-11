using System;
using System.Collections.Generic;
using System.Globalization;

namespace TaskManagement
{
    public class TaskCreator(List<Task> tasks)
    {
        private readonly List<Task> _tasks = tasks;

        public void CreateTask()
        {
            Console.Clear();
            Console.WriteLine("Create New Task");

            // bunch of data to enter. had to create "helper" functions because it got too messy
            string name = PromptHelper.PromptUntilValid(
                "Enter task name: ",
                input => !string.IsNullOrWhiteSpace(input),
                "Task name cannot be empty."
            );
            // enter priority
            string priority = PromptHelper.PromptUntilValid(
                "Enter task priority (High, Medium, Low): ",
                input =>
                    input.Equals("high", StringComparison.OrdinalIgnoreCase) ||
                    input.Equals("medium", StringComparison.OrdinalIgnoreCase) ||
                    input.Equals("low", StringComparison.OrdinalIgnoreCase),
                "Invalid priority. Please enter High, Medium, or Low."
            ).ToUpper();
            // enter due date
            DateTime dueDate = DateInputHelper.PromptRequiredDate(
                "Enter task due date and time (MM/dd/yyyy hh:mm tt): "
            );

            string category = PromptHelper.PromptUntilValid(
                "Enter task category (Personal, Work): ",
                input =>
                    input.Equals("personal", StringComparison.OrdinalIgnoreCase) ||
                    input.Equals("work", StringComparison.OrdinalIgnoreCase),
                "Invalid category. Please enter Personal or Work."
            ).ToUpper();

            DateTime? reminder = null;
            string reminderChoice = PromptHelper.PromptChoice(
                "Do you want to set a reminder? (Yes/No): ",
                ["yes", "no"]
            );

            if (reminderChoice == "yes")
            {
                reminder = DateInputHelper.PromptRequiredDate(
                    "Enter reminder date and time (MM/dd/yyyy hh:mm tt): "
                );
            }
            // save to task list
            _tasks.Add(new Task(name, priority, dueDate, category, reminder));
            Console.WriteLine("\nTask Created Successfully!");
        }
    }
}
