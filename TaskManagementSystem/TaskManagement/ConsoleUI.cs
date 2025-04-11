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
                        DisplayTasks();   // Call DisplayTasks to view all tasks
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

                // Wait for user to press Enter before returning to the menu
                if (running)
                {
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
                // Display each task in a simple, readable list format
                foreach (var task in _tasks)
                {
                    AnsiConsole.MarkupLine($"[bold green]Task Name:[/] {task.Name}");
                    AnsiConsole.MarkupLine($"[bold yellow]Priority:[/] {task.Priority}");
                    AnsiConsole.MarkupLine($"[bold blue]Due Date:[/] {task.DueDate.ToShortDateString()}");
                    AnsiConsole.MarkupLine($"[bold red]Status:[/] {(task.IsComplete ? "Completed" : "Incomplete")}");
                    AnsiConsole.MarkupLine(new string('-', 40)); // Separator line for clarity
                }
            }

            // Extract task names for selection
            var taskNames = _tasks.Select(task => task.Name).ToList();

            // Allow the user to scroll through and select a task by its name
            var selectedTaskName = AnsiConsole.Prompt(new SelectionPrompt<string>()
                .Title("Select a task to interact with")
                .AddChoices(taskNames)); // Use task names for selection

            // Find the corresponding Task object based on selected task name
            var selectedTask = _tasks.FirstOrDefault(t => t.Name == selectedTaskName);

            if (selectedTask != null)
            {
                // Display task options for the selected task
                var options = new[] { "Edit Task", "Delete Task", "Start Timer", "Return to Main Menu" };

                var actionChoice = AnsiConsole.Prompt(new SelectionPrompt<string>()
                    .AddChoices(options)
                    .Title("Select an option"));

                switch (actionChoice)
                {
                    case "Edit Task":
                        // Delegate the edit task functionality to TaskViewer
                        _taskViewer.EditTask(selectedTask);
                        break;
                    case "Delete Task":
                        // Delegate the delete task functionality to TaskViewer
                        _taskViewer.DeleteTask(selectedTask);
                        break;
                    case "Start Timer":
                        // Delegate the start timer functionality to TaskViewer
                        StartTimer(selectedTask);
                        break;
                    case "Return to Main Menu":
                        break;  // Return to the main menu
                    default:
                        AnsiConsole.MarkupLine("[bold red]Invalid option. Returning to task list.[/]");
                        break;
                }
            }
            else
            {
                AnsiConsole.MarkupLine("[bold red]Error: Task not found.[/]");
            }
        }

        public void StartTimer(Task task)
        {
            AnsiConsole.MarkupLine($"Starting timer for task: {task.Name}");
            TimeSpan timeSpent = _taskViewer.StartTimer(task);
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
