namespace TimeManagement
{
    public class Task
    {
        public string Name { get; set; }
        public string Priority { get; set; }
        public DateTime DueDate { get; set; }
        public string Category { get; set; }
        public bool IsComplete { get; set; }

        // Constructor to initialize the task
        public Task(string name, string priority, DateTime dueDate, string category)
        {
            Name = name;
            Priority = priority;
            DueDate = dueDate;
            Category = category;
            IsComplete = false;
        }

        // Method to display task information
        public void DisplayTask()
        {
            Console.WriteLine($"Task: {Name} | Priority: {Priority} | Due Date: {DueDate.ToShortDateString()} | Category: {Category} | Completed: {IsComplete}");
        }
    }
}
