using System;
using System.Collections.Generic;
using System.Linq;
using Spectre.Console;

namespace TaskManagement
{
    public class ConsoleUI(List<Task> tasks, TaskCreator taskCreator, TaskViewer taskViewer, ProductivitySummary productivitySummary, ReminderService reminderService)
    {
        private readonly List<Task> _tasks = tasks;
        private readonly TaskCreator _taskCreator = taskCreator;
        private readonly TaskViewer _taskViewer = taskViewer;
        private readonly ProductivitySummary _productivitySummary = productivitySummary;
        private readonly ReminderService _reminderService = reminderService;

        public void Start()
        {
            ShowReminderPopup();
            bool running = true;

            var menuActions = new Dictionary<string, Action>
            {
                ["Create New Task"] = () => { _taskCreator.CreateTask(); TaskManager.SaveTasks(_tasks); },
                ["Display Tasks"] = DisplayTasks,
                ["View Productivity Summary"] = ShowProductivitySummary,
                ["Exit"] = () => {
                    AnsiConsole.MarkupLine("[bold red]Goodbye![/]");
                    running = false;
                }
            };

            while (running)
            {
                AnsiConsole.Clear();
                AnsiConsole.MarkupLine("[bold blue]Welcome User![/]");

                string choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[bold yellow]Please choose an option:[/]")
                        .AddChoices(menuActions.Keys));

                menuActions[choice].Invoke();

                if (running)
                {
                    AnsiConsole.MarkupLine("\n[bold green]Press Enter to return to the menu...[/]");
                    Console.ReadLine();
                }
            }

            TaskManager.SaveTasks(_tasks);
            Environment.Exit(0);
        }

        public void DisplayTasks()
        {
            AnsiConsole.Clear();
            AnsiConsole.MarkupLine("[bold cyan]All Tasks[/]");

            if (_tasks.Count == 0)
            {
                AnsiConsole.MarkupLine("[bold red]No tasks available.[/]");
                return;
            }

            foreach (var task in _tasks)
            {
                AnsiConsole.MarkupLine($"[bold green]Task Name:[/] {task.Name}");
                AnsiConsole.MarkupLine($"[bold yellow]Priority:[/] {task.Priority}");
                AnsiConsole.MarkupLine($"[bold blue]Due Date:[/] {task.DueDate:MM/dd/yyyy hh:mm tt}");
                AnsiConsole.MarkupLine($"[bold red]Status:[/] {(task.IsComplete ? "Completed" : "Incomplete")}");
                AnsiConsole.MarkupLine(new string('-', 40));
            }

            var selectedTaskName = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Select a task to interact with")
                    .AddChoices(_tasks.Select(t => t.Name)));

            var selectedTask = _tasks.FirstOrDefault(t => t.Name == selectedTaskName);

            AnsiConsole.MarkupLine($"[bold blue]You have selected Task:[/][green] {selectedTask.Name}[/]");

            if (selectedTask != null)
                HandleTaskInteraction(selectedTask);
            else
                AnsiConsole.MarkupLine("[bold red]Error: Task not found.[/]");
        }

        private void HandleTaskInteraction(Task task)
        {
            var action = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Select an option")
                    .AddChoices("Edit Task", "Delete Task", "Start Task", "Return to Main Menu"));

            switch (action)
            {
                case "Edit Task":
                    TaskViewer.EditTask(task);
                    break;
                case "Delete Task":
                    _taskViewer.DeleteTask(task);
                    break;
                case "Start Task":
                    StartTask(task);
                    break;
                case "Return to Main Menu":
                    break;
            }
        }

        public void StartTask(Task task)
        {
            if (task.IsComplete)
            {
                AnsiConsole.MarkupLine($"[yellow]This task '{task.Name}' is already completed and cannot be started again.[/]");
                return;
            }

            bool startAnother = true;

            while (startAnother)
            {
                AnsiConsole.MarkupLine($"[bold cyan]Starting timer for task:[/] [green]{task.Name}[/]");
                TimeSpan timeSpent = TaskViewer.StartTimer(task);

                if (timeSpent > TimeSpan.Zero)
                {
                    AnsiConsole.MarkupLine($"[bold green]Total time added to this task: {timeSpent}[/]");
                    TaskManager.SaveTasks(_tasks);
                }

                var answer = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Do you want to start another task?")
                        .AddChoices("Yes", "No"));

                if (answer == "Yes")
                {
                    DisplayTasks();
                    return;
                }
                else
                {
                    AnsiConsole.MarkupLine("[yellow]Returning to main menu...[/]");
                    startAnother = false;
                }
            }
        }

        public void ShowProductivitySummary()
        {
            var summary = _productivitySummary.GenerateSummaryData();

            AnsiConsole.Clear();
            AnsiConsole.MarkupLine("[bold underline yellow]===== Productivity Summary =====[/]");
            AnsiConsole.MarkupLine($"[green]Total Tasks:[/]        {summary.TotalTasks}");
            AnsiConsole.MarkupLine($"[green]Completed Tasks:[/]    {summary.CompletedTasks}");
            AnsiConsole.MarkupLine($"[bold red]Overdue Tasks:[/]      {summary.OverdueTasks}");
            AnsiConsole.MarkupLine($"[green]Time Tracked:[/]       {summary.TimeTracked:hh\\:mm\\:ss}");

            AnsiConsole.WriteLine();
            AnsiConsole.MarkupLine("[bold cyan]---- Tasks by Priority ----[/]");

            foreach (var data in summary.TasksByPriority)
            {
                AnsiConsole.MarkupLine($"[blue]{data.Key} Priority:[/] {data.Value} task(s)");
            }

            AnsiConsole.WriteLine();
            AnsiConsole.MarkupLine("[bold purple_1]---- Tasks by Category ----[/]");

            foreach (var data in summary.TasksByCategory)
            {
                AnsiConsole.MarkupLine($"[deeppink4_1]{data.Key} Category:[/] {data.Value} task(s)");
            }

            AnsiConsole.MarkupLine("[bold cyan]===============================[/]");

            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Return to Main Menu?")
                    .AddChoices("Yes", "No"));

            if (choice == "Yes")
                AnsiConsole.MarkupLine("[green]Returning to Main Menu...[/]");
            else
                AnsiConsole.MarkupLine("[yellow]You can continue viewing the summary.[/]");
        }

        private void ShowReminderPopup()
        {
            var reminders = _reminderService.GetDueReminders();

            if (reminders.Count != 0)
            {
                AnsiConsole.MarkupLine("[bold red]🔔 You have task reminders![/]\n");

                foreach (var task in reminders)
                {
                    AnsiConsole.MarkupLine($"[yellow]- {task.Name}[/] [dim](Reminder: {task.Reminder.Value:MM/dd/yyyy hh:mm tt})[/]");
                }

                AnsiConsole.MarkupLine("\n[green]Press Enter to continue...[/]");
                Console.ReadLine();
            }
        }
        
    }
}

