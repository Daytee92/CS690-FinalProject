using System.Runtime.CompilerServices;

namespace TaskManagement
{
    public class Task(string name, string priority, DateTime dueDate, string category, DateTime? reminder = null, TimeSpan timeSpent = default)
    {
        public string Name { get; set; } = name;
        public string Priority { get; set; } = priority;
        public DateTime DueDate { get; set; } = dueDate;
        public string Category { get; set; } = category;
        public bool IsComplete { get; set; } = false;
        public DateTime? Reminder { get; set; } = reminder;
        public TimeSpan TimeSpent { get; set; } = timeSpent;
        
    }
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
            var reminderService = new ReminderService(tasks);

            // Initialize Console UI class and start the UI
            var consoleUI = new ConsoleUI(tasks, taskCreator, taskViewer, productivitySummary, reminderService);
            consoleUI.Start();  // Start the interactive UI
        }
    }
}