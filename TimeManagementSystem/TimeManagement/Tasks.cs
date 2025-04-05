namespace TimeManagement
{
    public class Task
    {
        public string Name { get; set; }
        public string Priority { get; set; }
        public DateTime DueDate { get; set; }
        public string Category { get; set; }
        public bool IsComplete { get; set; }
        public DateTime? Reminder { get; set; }

        // Constructor to initialize the task
        public Task(string name, string priority, DateTime dueDate, string category, DateTime? reminder = null)
        {
            Name = name;
            Priority = priority;
            DueDate = dueDate;
            Category = category;
            IsComplete = false;
            Reminder = reminder;

        }

        // Display task information
        public void DisplayTask()
        {
            Console.WriteLine($"Task Name: {Name}");
            Console.WriteLine($"Priority: {Priority}");
            Console.WriteLine($"Due Date: {DueDate.ToShortDateString()}");

            if (Reminder.HasValue)
            {
                Console.WriteLine($"Reminder: {Reminder.Value.ToShortDateString()} at {Reminder.Value.ToShortTimeString()}");
            }
            else
            {
                Console.WriteLine("Reminder: No reminder set.");
            }

            Console.WriteLine($"Completed: {(IsComplete ? "Yes" : "No")}");
        }
    }
}
