using System;
using System.Collections.Generic;
using Spectre.Console;

namespace TaskManagement
{
    public class ConsoleUI
    {
        private List<Task> _tasks;
        private TaskCreator _taskCreator;
        private TaskViewer _taskViewer;

        public ConsoleUI(List<Task> tasks, TaskCreator taskCreator, TaskViewer taskViewer)
        {
            _tasks = tasks;
            _taskCreator = taskCreator;
            _taskViewer = taskViewer;
        }

        public void Start()
        {
            bool running = true;

            while (running)
            {
                // Clear the screen and add a fancy header
                AnsiConsole.Clear();
                AnsiConsole.MarkupLine("[bold blue]Welcome User![/]");

                // Display main menu with choices
                var choices = new string[]
                {
                    "Create New Task",
                    "Display Tasks",
                    "View Productivity Summary",
                    "Exit"
                };

                var menu = new SelectionPrompt<string>()
                    .Title("[bold yellow]Please choose an option:[/]")
                    .AddChoices(choices);

                string choice = AnsiConsole.Prompt(menu);

                switch (choice)
                {
                    case "Create New Task":
                        _taskCreator.CreateTask();  // Call CreateTask to add a new task
                        TaskManager.SaveTasks(_tasks);  // Save tasks to the JSON file
                        break;
                    case "Display Tasks":
                        DisplayTasks();   // Call DisplayTasks from ConsoleUI to view all tasks
                        break;
                    case "View Productivity Summary":
                        ViewProductivitySummary(); // Placeholder for Productivity Summary
                        break;
                    case "Exit":
                        running = false;  // Exit the program
                        break;
                    default:
                        AnsiConsole.MarkupLine("[bold red]Invalid option. Please try again.[/]");
                        break;
                }

                // Only ask the user to press Enter if the program is not exiting
                if (running)
                {
                    // Wait for user to press Enter before returning to the menu
                    AnsiConsole.MarkupLine("\n[bold green]Press Enter to return to the menu...[/]");
                    Console.ReadLine();
                }
            }

            // Save tasks when the program exits
            TaskManager.SaveTasks(_tasks);  // Save tasks to the JSON file before exiting
            Environment.Exit(0);  // Exit the program immediately
        }

        // Display tasks and task options (using AnsiConsole)
        public void DisplayTasks()
        {
            AnsiConsole.Clear();
            AnsiConsole.MarkupLine("[bold cyan]All Tasks[/]");

            if (_tasks.Count == 0)
            {
                AnsiConsole.MarkupLine("[bold red]No tasks available.[/]");
            }
            else
            {
                // Display each task
                foreach (var task in _tasks)
                {
                    AnsiConsole.MarkupLine($"[bold green]Task Name:[/] {task.Name}");
                    AnsiConsole.MarkupLine($"[bold yellow]Priority:[/] {task.Priority}");
                    AnsiConsole.MarkupLine($"[bold blue]Due Date:[/] {task.DueDate.ToShortDateString()}");
                    AnsiConsole.MarkupLine($"[bold red]Status:[/] {(task.IsComplete ? "Completed" : "Incomplete")}");
                    AnsiConsole.MarkupLine(new string('-', 40)); 
                }
            }

            // Select task
            var selectedTask = AnsiConsole.Prompt(new SelectionPrompt<Task>()
                .Title("Select a task to interact with")
                .AddChoices(_tasks));

            // Display task options for the selected task
            var options = new[]
            { "Edit Task",
            "Delete Task",
            "Start Task",
            "Return to Main Menu" };

            var actionChoice = AnsiConsole.Prompt(new SelectionPrompt<string>()
                .AddChoices(options)
                .Title("Select an option"));

            switch (actionChoice)
            {
                case "Edit Task":
                    EditTask(selectedTask);
                    break;
                case "Delete Task":
                    DeleteTask(selectedTask);
                    break;
                case "Start Task":
                    StartTask(selectedTask);
                    break;
                case "Return to Main Menu":
                    break;  // Return to the main menu
                default:
                    AnsiConsole.MarkupLine("[bold red]Invalid option. Returning to task list.[/]");
                    break;
            }
        }

        // Edit task
        public void EditTask(Task task)
        {
            string newName = AnsiConsole.Ask<string>($"Current Task Name: {task.Name}\nEnter new name (or press Enter to keep current): ");
            string newPriority = AnsiConsole.Ask<string>($"Current Priority: {task.Priority}\nEnter new priority (High, Medium, Low): ");
            string dueDateInput = AnsiConsole.Ask<string>($"Current Due Date: {task.DueDate.ToShortDateString()}\nEnter new due date (YYYY-MM-DD): ");
            bool isComplete = AnsiConsole.Confirm("Mark as complete?");

            if (!string.IsNullOrEmpty(newName)) task.Name = newName;

            DateTime newDueDate;
            if (!string.IsNullOrEmpty(newName) && DateTime.TryParse(dueDateInput, out newDueDate))
            {
                _taskViewer.EditTask(task, newName, newPriority, newDueDate, isComplete);
                AnsiConsole.MarkupLine("[bold green]Task updated successfully![/]");
            }
            else
            {
                AnsiConsole.MarkupLine("[bold red]Invalid input. Task not updated.[/]");
            }
        }

        // Delete task in ConsoleUI (taskViewer does the logic)
        public void DeleteTask(Task task)
        {
            var confirmation = AnsiConsole.Confirm($"Are you sure you want to delete the task: {task.Name}?");
            if (confirmation)
            {
                _taskViewer.DeleteTask(task);
                AnsiConsole.MarkupLine("[bold green]Task deleted successfully.[/]");
            }
            else
            {
                AnsiConsole.MarkupLine("[bold red]Task not deleted.[/]");
            }
        }

        // Start timer in ConsoleUI (taskViewer does the logic)
        public void StartTask(Task task)
        {
            AnsiConsole.MarkupLine($"Starting timer for task: {task.Name}");
            TimeSpan timeSpent = _taskViewer.StartTask(task);
            AnsiConsole.MarkupLine($"[bold green]Total time spent on task: {timeSpent}[/]");
        }

        // Placeholder for Productivity Summary functionality
        public void ViewProductivitySummary()
        {
            AnsiConsole.Clear();
            AnsiConsole.MarkupLine("[bold yellow]Productivity Summary - Coming Soon![/]");
        }
    }
}
