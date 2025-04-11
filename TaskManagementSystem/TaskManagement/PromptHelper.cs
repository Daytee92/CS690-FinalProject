using System;

namespace TaskManagement
{
    public static class PromptHelper
    {
        public static string PromptUntilValid(string prompt, Func<string, bool> validator, string errorMessage)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(input) && validator(input)) //had this isnullorwhitespace everywhere in my create task method
                    return input;

                Console.WriteLine(errorMessage);
            }
        }

        public static string PromptEdit(string prompt, string currentValue, Func<string, bool>? validator = null, string errorMessage = "")
        {
            //this is for the edit task class since they can leave the edit empty
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                // Pressing Enter keeps the current value
                if (string.IsNullOrWhiteSpace(input))
                    return currentValue;

                // If valid input or no validator needed
                if (validator == null || validator(input))
                    return input;

                Console.WriteLine(errorMessage);
            }
        }
        public static string PromptChoice(string prompt, string[] validChoices)
        {   //need to figure out if this one is necessary
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine()?.Trim().ToLower();

                foreach (var choice in validChoices)
                {
                    if (string.Equals(input, choice, StringComparison.OrdinalIgnoreCase))
                        return choice;
                }

                Console.WriteLine("Invalid input. Please try again.");
            }
        }
    }
}
