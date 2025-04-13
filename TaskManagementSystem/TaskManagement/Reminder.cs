using System;
using System.Collections.Generic;
using System.Linq;

namespace TaskManagement
{
    public class ReminderService
    {
        private readonly List<Task> _tasks;

        public ReminderService(List<Task> tasks)
        {
            _tasks = tasks;
        }

        public List<Task> GetDueReminders()
        {
            return _tasks
                .Where(t =>
                    t.Reminder.HasValue &&
                    !t.IsComplete &&
                    t.Reminder.Value <= DateTime.Now)
                .ToList();
        }
    }
}
