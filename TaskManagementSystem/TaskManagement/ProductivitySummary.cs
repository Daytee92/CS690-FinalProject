using System;
using System.Collections.Generic;

namespace TaskManagement
{
    public class ProductivitySummary
    {
        private List<Task> _tasks;

        public ProductivitySummary(List<Task> tasks)
        {
            _tasks = tasks;
        }

        using System;
using System.Collections.Generic;
using System.Linq;

namespace TaskManagement
{
    public class ProductivitySummary
    {
        private readonly List<Task> _tasks;

        public ProductivitySummary(List<Task> tasks)
        {
            _tasks = tasks;
        }

        public void GenerateProductivitySummary()
        {
            int totalTasks = _tasks.Count;
            int completedTasks = _tasks.Count(t => t.IsComplete);
            int overdueTasks = _tasks.Count(t => !t.IsComplete && t.DueDate < DateTime.Now);
            TimeSpan totalTimeTracked = new TimeSpan(_tasks.Sum(t => t.TimeSpent.Ticks));

            Console.WriteLine("===== Productivity Summary =====");
            Console.WriteLine($"Total Tasks:        {totalTasks}");
            Console.WriteLine($"Completed Tasks:    {completedTasks}");
            Console.WriteLine($"Overdue Tasks:      {overdueTasks}");
            Console.WriteLine($"Time Tracked:       {totalTimeTracked:hh\\:mm\\:ss}");
            Console.WriteLine();

            Console.WriteLine("---- Tasks by Priority ----");

            var groupedByPriority = _tasks
                .GroupBy(t => t.Priority)
                .OrderBy(g => g.Key);

            foreach (var group in groupedByPriority)
            {
                Console.WriteLine($"{group.Key} Priority: {group.Count()} task(s)");
            }

            Console.WriteLine("===============================");

            Console.WriteLine("Return to Main Menu? (Y/N): ");
            string choice = Console.ReadLine().ToLower();
            if (choice == "y")
            {
                Console.WriteLine("Returning to Main Menu...");
                // The actual return will be handled by ConsoleUI
            }
            else
            {
                Console.WriteLine("You can continue viewing the summary.");
            }
        }
    }
}