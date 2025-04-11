using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using TaskManagement;
using Xunit;

namespace TaskManagement.Tests
{
    public class TaskManagerTests : IDisposable
    {
        private readonly string _testFilePath = "tasks.json";

        public TaskManagerTests()
        {
            if (File.Exists(_testFilePath))
                File.Delete(_testFilePath);
        }

        [Fact]
        public void SaveTasksTest()
        {
            // Arrange
            var tasks = new List<Task>
            {
                new("Sleep", "High", DateTime.Now.AddDays(1), "Work"),
                new("Run", "Low", DateTime.Now.AddDays(2), "Personal")
            };

            // Act
            TaskManager.SaveTasks(tasks);

            // Assert
            Assert.True(File.Exists(_testFilePath));

            string json = File.ReadAllText(_testFilePath);
            var loadedtasks = JsonConvert.DeserializeObject<List<Task>>(json);

            Assert.NotNull(loadedtasks);
            Assert.Equal(2, loadedtasks.Count);
            Assert.Equal("Sleep", loadedtasks[0].Name);
            Assert.Equal("Run", loadedtasks[1].Name);
            Assert.Equal("Low", loadedtasks[1].Priority);
            Assert.Equal("Work", loadedtasks[0].Category);
        }

        [Fact]
        public void LoadTasks_TestEmpty()
        {
            // Make sure file doesn't exist
            if (File.Exists(_testFilePath))
                File.Delete(_testFilePath);

            var tasks = TaskManager.LoadTasks();

            Assert.NotNull(tasks);
            Assert.Empty(tasks);
        }

        [Fact]
        public void SaveTasks_EmptyList_CreatesEmptyJson()
        {
            var emptyList = new List<Task>();

            TaskManager.SaveTasks(emptyList);

            Assert.True(File.Exists(_testFilePath));

            string json = File.ReadAllText(_testFilePath);
            var loaded = JsonConvert.DeserializeObject<List<Task>>(json);

            Assert.NotNull(loaded);
            Assert.Empty(loaded);
        }

        public void Dispose()
        {
            if (File.Exists(_testFilePath))
                File.Delete(_testFilePath);
        }
    }
}
