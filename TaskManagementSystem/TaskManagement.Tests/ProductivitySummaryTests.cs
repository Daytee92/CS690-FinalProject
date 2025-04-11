using System;
using System.Collections.Generic;
using TaskManagement;
using Xunit;

namespace TaskManagement.Tests
{
    public class ProductivitySummaryTests
    {
        [Fact]
        public void GenerateSummaryData_ReturnsCorrectCounts()
        {
            // Arrange
            var tasks = new List<Task>
            {
                new Task("Sleep", "High", DateTime.Now.AddDays(1), "Work") { IsComplete = true, TimeSpent = TimeSpan.FromHours(2) },
                new Task("Run", "Low", DateTime.Now.AddDays(-1), "Personal") { IsComplete = false, TimeSpent = TimeSpan.FromMinutes(30) },
                new Task("Play", "High", DateTime.Now.AddDays(3), "Work") { IsComplete = false, TimeSpent = TimeSpan.FromHours(1) }
            };

            var summary = new ProductivitySummary(tasks);

            // Act
            var result = summary.GenerateSummaryData();

            // Assert
            Assert.Equal(3, result.TotalTasks);               // Total
            Assert.Equal(1, result.CompletedTasks);           // 1 completed
            Assert.Equal(1, result.OverdueTasks);             // 1 overdue (Task B)
            Assert.Equal(TimeSpan.FromHours(3.5), result.TimeTracked); // 2 + 0.5 + 1 hours

            Assert.True(result.TasksByPriority.ContainsKey("High"));
            Assert.Equal(2, result.TasksByPriority["High"]);
            Assert.Equal(1, result.TasksByPriority["Low"]);
        }

        [Fact]
        public void GenerateSummaryData_WhenNoTasks_ReturnsZeroStats()
        {
            // Arrange
            var tasks = new List<Task>();
            var summary = new ProductivitySummary(tasks);

            // Act
            var result = summary.GenerateSummaryData();

            // Assert
            Assert.Equal(0, result.TotalTasks);
            Assert.Equal(0, result.CompletedTasks);
            Assert.Equal(0, result.OverdueTasks);
            Assert.Equal(TimeSpan.Zero, result.TimeTracked);
            Assert.Empty(result.TasksByPriority);
        }

        [Fact]
        public void GenerateSummaryData_AllTasksCompleted_NoOverdue()
        {
            // Arrange
            var tasks = new List<Task>
            {
                new Task("Task X", "Medium", DateTime.Now.AddDays(-5), "Work") { IsComplete = true },
                new Task("Task Y", "Medium", DateTime.Now.AddDays(-2), "Personal") { IsComplete = true }
            };

            var summary = new ProductivitySummary(tasks);

            // Act
            var result = summary.GenerateSummaryData();

            // Assert
            Assert.Equal(2, result.TotalTasks);
            Assert.Equal(2, result.CompletedTasks);
            Assert.Equal(0, result.OverdueTasks);  // None are incomplete
            Assert.Equal(2, result.TasksByPriority["Medium"]);
        }
    }
}
