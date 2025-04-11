using System;
using System.Collections.Generic;
using TaskManagement;

namespace TaskManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Load tasks from file immediately when the program starts
            List<Task> tasks = TaskManager.LoadTasks();  // Load tasks from tasks.json

            // Initialize task creator and viewer
            var taskCreator = new TaskCreator(tasks);
            var taskViewer = new TaskViewer(tasks);
            var productivitySummary = new ProductivitySummary(tasks);

            // Initialize Console UI class and start the UI
            var consoleUI = new ConsoleUI(tasks, taskCreator, taskViewer, productivitySummary);
            consoleUI.Start();  // Start the interactive UI
        }
    }
}