using System;
using System.Collections.Generic;
using TaskManagement;

namespace TaskManagement.Tests
{
    public class TaskCreatorTests
    {
        [Fact]
        public void TestCreateTask()
        {
            // Add tasks
            var tasks = new List<Task>();
            var taskCreator = new TaskCreator(tasks);
            string taskName = "Test Task";
            string priority = "High";
            DateTime dueDate = new DateTime(2025, 12, 31);
            string category = "Work";
            DateTime? reminder = new DateTime(2025, 12, 30, 9, 0, 0);  // Optional reminder

    
            taskCreator.CreateTask(taskName, priority, dueDate, category, reminder);

            // Assert
            Assert.Single(tasks);  // Ensures there's only one task in the list
            var task = tasks[0];
            Assert.Equal(taskName, task.Name);
            Assert.Equal(priority, task.Priority);
            Assert.Equal(dueDate, task.DueDate);
            Assert.Equal(category, task.Category);
            Assert.Equal(reminder, task.Reminder);  // Check reminder
        }
    }
}