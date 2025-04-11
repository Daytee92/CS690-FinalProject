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
                    .GroupBy(t => t.Priority)
                    .OrderBy(g => g.Key)
                    .ToDictionary(g => g.Key, g => g.Count())
            };
        }
    }
    
}