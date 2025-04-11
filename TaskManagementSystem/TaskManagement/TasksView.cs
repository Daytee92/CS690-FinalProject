using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace TaskManagement
{
    public class TaskViewer(List<Task> tasks)
    {
        private readonly List<Task> _tasks = tasks;

        // Edit a task
        public static void EditTask(Task task)
        {
            // Name
            string newName = PromptHelper.PromptEdit(
                "Enter new name or press Enter to keep current name: ",
                task.Name
            );

            // Priority
            string newPriority = PromptHelper.PromptEdit(
                "Enter new priority (High, Medium, Low) or press Enter to keep current: ",
                task.Priority,
                input => input.Equals("high", StringComparison.OrdinalIgnoreCase) ||
                         input.Equals("medium", StringComparison.OrdinalIgnoreCase) ||
                         input.Equals("low", StringComparison.OrdinalIgnoreCase),
                "Invalid priority. Keeping current."
            );

            // Due date
            DateTime newDueDate = DateInputHelper.PromptOptionalDate(
                "Enter new due date and time (MM/dd/yyyy hh:mm tt) or press Enter to keep current: ",
                task.DueDate
            );

            // Completion
            Console.WriteLine($"Current Status: {(task.IsComplete ? "Completed" : "Incomplete")}");
            string complete = PromptHelper.PromptChoice(
                "Mark task as complete? (Yes/No): ",
                ["yes", "no"]);

            // Apply changes
            task.Name = newName;
            task.Priority = newPriority;
            task.DueDate = newDueDate;
            task.IsComplete = complete == "yes";

            Console.WriteLine("Task updated successfully!");
        }

        // Delete a task
        public void DeleteTask(Task task)
        {
            string confirm = PromptHelper.PromptChoice(
                "Are you sure you want to delete the selected task? (Yes/No): ",
                ["yes", "no"]);

            if (confirm == "yes")
            {
                _tasks.Remove(task);
                Console.WriteLine("Task deleted successfully.");
            }
            else
            {
                Console.WriteLine("Task not deleted.");
            }
        }

        // Start timer for a task
        public static TimeSpan StartTimer(Task task)
        {
            if (task.IsComplete)
            {
                Console.WriteLine($"You already completed the task '{task.Name}'.");
                return TimeSpan.Zero;
            }

            string start = PromptHelper.PromptChoice(
                "Start task now? (Yes/No): ",
                ["yes", "no"]);

            if (start != "yes")
            {
                Console.WriteLine("Timer not started.");
                return TimeSpan.Zero;
            }

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Console.Write("Press Enter to stop the timer...");
            Console.ReadLine();
            stopwatch.Stop();

            Console.WriteLine($"Time elapsed: {stopwatch.Elapsed}");
            task.TimeSpent += stopwatch.Elapsed;

            string done = PromptHelper.PromptChoice(
                "Did you complete this task? (Yes/No): ",
                ["yes", "no"]);

            task.IsComplete = done == "yes";

            Console.WriteLine(task.IsComplete
                ? $"{task.Name} marked as complete."
                : $"{task.Name} left as incomplete.");

            return stopwatch.Elapsed;
        }

        
    } 
}

