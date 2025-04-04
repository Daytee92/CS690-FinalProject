namespace TimeManagementSystem.Tests;
using TimeManagement;
using System;
using System.Collections.Generic;

namespace TaskManager
{
    public class TaskManager
    {
        // Helper method to simulate Console.ReadLine()
        private static void SimulateInput(string input)
        {
            var stringReader = new System.IO.StringReader(input);
            Console.SetIn(stringReader);
        }

        [Fact]
        public void CreateTask_ShouldAddTaskToList()
        {
            // Arrange: Prepare a TaskManager instance and simulated input
            var taskManager = new TaskManager();
            string taskName = "Complete Project";
            string priority = "high";
            string dueDate = "12/12/2025";
            string category = "work";
            

            SimulateInput($"{taskName}\n{priority}\n{dueDate}\n{category}\n");

            // Act: Run the CreateTask method
            taskManager.CreateTask();

            // Assert: Verify that the task was added to the list
            var task = taskManager.GetTasks()[0];  // Get the first task
            Assert.Equal(taskName, task.Name);
            Assert.Equal(priority.ToUpper(), task.Priority);
            Assert.Equal(DateTime.Parse(dueDate), task.DueDate);
            Assert.Equal(category.ToUpper(), task.Category);
        }

        //
    }
}
