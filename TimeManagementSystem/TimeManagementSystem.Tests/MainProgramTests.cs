using System;
using System.IO;
using Xunit;
using TimeManagement;

namespace TimeManagementTests
{
    public class ProgramTests
    {
        // Helper method to simulate Console.ReadLine() and capture Console.WriteLine() output
        private string RunProgram(string input)
        {
            // Setup: Create a StringWriter to capture Console output
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);  // Redirect Console.WriteLine to StringWriter

            // Setup: Create a StringReader to simulate user input
            var stringReader = new StringReader(input);
            Console.SetIn(stringReader);  // Redirect Console.ReadLine to StringReader

            // Run: Start the program (main logic)
            Program.Main(new string[] { });

            return stringWriter.ToString();  // Return the captured output
        }

        [Fact]
        public void CreateTask_ShouldShowTaskCreatedMessage()
        {
            // Simulate user input: "1" for Create New Task, followed by task details and press Enter after task creation
            string input = "1\nComplete report\nHigh\n04/10/2025\nWork\n\n";

            // Run the program and capture the output
            string output = RunProgram(input);

            // Assert: Check if the task creation confirmation message is shown
            Assert.Contains("Task Created Successfully!", output);
            Assert.Contains("Press any key to return to the menu...", output);
        }

        [Fact]
        public void ViewTasks_ShouldShowTasksList()
        {
            // Simulate user input: "2" for View All Tasks, then press Enter
            string input = "2\n";

            // Run the program and capture the output
            string output = RunProgram(input);

            // Assert: Check if the task list is displayed
            Assert.Contains("All Tasks", output);  // Ensure the header is displayed
            Assert.Contains("Task: Complete report", output);  // Check if a task is listed
            Assert.Contains("Press any key to return to the menu...", output);
        }

        [Fact]
        public void MainMenu_InvalidOption_ShouldShowErrorMessage()
        {
            // Simulate user input: Invalid input "99" then "Enter"
            string input = "99\n";

            // Run the program and capture the output
            string output = RunProgram(input);

            // Assert: Check if the error message is shown
            Assert.Contains("Invalid option. Please try again.", output);
            Assert.Contains("Press any key to return to the menu...", output);
        }

        [Fact]
        public void ExitOption_ShouldExitProgram()
        {
            // Simulate user input: "4" to exit the program
            string input = "4\n";

            // Run the program and capture the output
            string output = RunProgram(input);

            // Assert: Check if program exits without errors
            Assert.DoesNotContain("Please choose an option:", output);  // Menu should not be shown after exit
        }
    }
}
