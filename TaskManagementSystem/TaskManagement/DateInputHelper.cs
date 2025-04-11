using System;
using System.Globalization;

namespace TaskManagement
{
    public static class DateInputHelper
    {
        //creaated this one to reduce redundancy but might not be needed
        public static DateTime PromptOptionalDate(string prompt, DateTime currentDate)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
                return currentDate;

            if (DateTime.TryParseExact(input, "MM/dd/yyyy hh:mm tt", null, DateTimeStyles.None, out DateTime parsedDate))
                return parsedDate;

            Console.WriteLine("Invalid date. Keeping current due date.");
            return currentDate;
        }

        public static DateTime PromptRequiredDate(string prompt)
        {
            //same with this one. Might just move back to create task and edit task docs
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (DateTime.TryParseExact(input, "MM/dd/yyyy hh:mm tt", null, DateTimeStyles.None, out var date))
                {
                    if (date > DateTime.Now)
                        return date;

                    Console.WriteLine("Date and time must be in the future.");
                }
                else
                {
                    Console.WriteLine("Invalid format. Please use MM/dd/yyyy hh:mm tt (e.g., 04/25/2025 03:30 PM).");
                }
            }
        }
    }
}
