using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace TaskManagement
{
    public class TaskViewer
    {
        private List<Task> _tasks;

        public TaskViewer(List<Task> tasks)
        {
            _tasks = tasks;
        }

        // Method to view tasks (doesn't include AnsiConsole, just task logic)
        public List<Task> GetAllTasks()
        {
            return _tasks;
        }

        // Edit task logic
        public void EditTask(Task task, string newName, string newPriority, DateTime newDueDate, bool isComplete)
        {
            if (!string.IsNullOrEmpty(newName))
                task.Name = newName;

            if (newPriority == "high" || newPriority == "medium" || newPriority == "low")
                task.Priority = newPriority.ToUpper();

            task.DueDate = newDueDate;
            task.IsComplete = isComplete;
        }

        // Delete task logic
        public void DeleteTask(Task task)
        {
            _tasks.Remove(task);
        }

        // Start timer logic
        public TimeSpan StartTask(Task task)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Console.ReadLine();  // Waiting for user to press Enter to stop the timer

            stopwatch.Stop();
            return stopwatch.Elapsed;
        }
    }
}
