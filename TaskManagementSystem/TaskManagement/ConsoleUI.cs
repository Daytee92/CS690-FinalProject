using System;
using System.Collections.Generic;
using Spectre.Console;
using TaskManagement;

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
                // Clear the screen and add a header
                AnsiConsole.Clear();
                AnsiConsole.MarkupLine("[bold purple]Welcome User![/]");

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
                        _taskViewer.ViewTasks();   // Call ViewTasks to view all tasks
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
                if (running) //To ensure when the user presses quit, it exits the program
                {
                    AnsiConsole.MarkupLine("\n[bold green]Press Enter to return to the menu...[/]");
                    Console.ReadLine();
                }

            }

            // Save tasks when the program exits
            TaskManager.SaveTasks(_tasks);  // Save tasks to the JSON file before exiting
            Environment.Exit(0);
        }

        // Placeholder for Productivity Summary functionality
        public void ViewProductivitySummary()
        {
            AnsiConsole.Clear();
            AnsiConsole.MarkupLine("[bold yellow]Productivity Summary - Coming Soon![/]");

            // For later
            
            AnsiConsole.MarkupLine("\n[bold cyan]Tasks Summary:[/]");
            AnsiConsole.MarkupLine($"Total Tasks: {_tasks.Count}");
            int completedTasks = _tasks.Count(t => t.IsComplete);
            int incompleteTasks = _tasks.Count(t => !t.IsComplete);
            AnsiConsole.MarkupLine($"Completed Tasks: {completedTasks}");
            AnsiConsole.MarkupLine($"Incomplete Tasks: {incompleteTasks}");

        }
    }
}
