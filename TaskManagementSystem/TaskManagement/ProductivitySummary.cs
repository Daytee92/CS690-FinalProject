using System;
using System.Collections.Generic;
using System.Linq;

namespace TaskManagement
{
    public class SummaryStats
    {
        public int TotalTasks { get; set; }
        public int CompletedTasks { get; set; }
        public int OverdueTasks { get; set; }
        public TimeSpan TimeTracked { get; set; }
        public required Dictionary<string, int> TasksByPriority { get; set; }
        public required Dictionary<string, int> TasksByCategory { get; set; }
    }
    public class ProductivitySummary(List<Task> tasks)
    {
        private readonly List<Task> _tasks = tasks;

        public SummaryStats GenerateSummaryData()
        {
            //option to add number of work vs personal? need to figure if that is beneficial
            return new SummaryStats
            {
                TotalTasks = _tasks.Count,
                CompletedTasks = _tasks.Count(t => t.IsComplete),
                OverdueTasks = _tasks.Count(t => !t.IsComplete && t.DueDate < DateTime.Now),
                TimeTracked = new TimeSpan(_tasks.Sum(t => t.TimeSpent.Ticks)),
                TasksByPriority = _tasks
                    .GroupBy(t => t.Priority.ToUpperInvariant())
                    .OrderBy(g => g.Key)
                    .ToDictionary(g => Capitalize(g.Key), g => g.Count()),

                TasksByCategory = _tasks
                    .GroupBy(t => t.Category.ToUpperInvariant())
                    .OrderBy(g => g.Key)
                    .ToDictionary(g => Capitalize(g.Key), g => g.Count())
                

            };
        }
        private static string Capitalize(string input)
        {
            return string.IsNullOrWhiteSpace(input)
                ? input
                : char.ToUpper(input[0]) + input.Substring(1).ToLower();
        }
    }
}