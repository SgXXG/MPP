using System;
using NUnit.Framework;
using MPP_Lab1;
using System.Threading.Tasks;

namespace Tests_Lab1
{
    public class UnitTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CreateList()
        {
            int threadsNumber = 1;
            TaskQueue taskQueue = new TaskQueue(threadsNumber);
            Assert.IsNotNull(taskQueue);
        }

        [Test]
        public void CountOfCreatedElementsInList()
        {
            int threadsNumber = 0;
            TaskQueue taskQueue = new TaskQueue(threadsNumber);
            Assert.Zero(taskQueue.threads.Count);
        }

        [Test]
        public void AddTaskInQueue()
        {
            int threadsNumber = 2;
            TaskQueue taskQueue = new TaskQueue(threadsNumber);
            taskQueue.taskEnqueue(() => Console.WriteLine($"Hello from {Thread.CurrentThread.Name}"));
            Assert.NotZero(taskQueue.tasks.Count);
        }

        [Test]
        public void CheckCountAfterCompletingTasks()
        {
            int threadsNumber = 2;
            int tasks = 2;
            TaskQueue taskQueue = new TaskQueue(threadsNumber);

            for (int i = 0; i < tasks; i++)
            {
                taskQueue.taskEnqueue(() => Console.WriteLine($"Hello from {Thread.CurrentThread.Name}"));
            }

            taskQueue.Abort();
            taskQueue.Wait();

            Assert.AreEqual(taskQueue.tasks.Count, 0);
        }
    }
}