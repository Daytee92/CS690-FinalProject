namespace TimeManagement;
using System;
public class Task
{
    public string Name { get; set; }
    public string Priority { get; set; }
    public DateTime DueDate { get; set; }
    public string Category { get; set; }
    public bool IsComplete { get; set; }  // New property to track if the task is completed

    public Task(string name, string priority, DateTime dueDate, string category)
    {
        Name = name;
        Priority = priority;
        DueDate = dueDate;
        Category = category;
        IsComplete = false;
    }

    // Optional: You can add a method to display task information if needed.
    public void DisplayTask()
    {
        Console.WriteLine($"Task: {Name} | Priority: {Priority} | Due Date: {DueDate.ToShortDateString()} | Category: {Category} | Completed: {IsComplete}");
    }
}
