using System;
using System.Threading;
using NLog;

namespace MPP_Lab1;

class Program {

    static int Main(string[] args) {

        var logger = LogManager.GetCurrentClassLogger();

        try {

            Console.WriteLine("Enter the number of threads: ");
            if (!int.TryParse(Console.ReadLine(), out int threads)) {

                Console.WriteLine("Ошибка ввода");
                logger.Error("Stopped program because of exception");
                return -1;
            }

            Console.WriteLine("Enter the number of tasks: ");
            if (!int.TryParse(Console.ReadLine(), out int tasks)) {

                Console.WriteLine("Ошибка ввода");
                logger.Error("Stopped program because of exception");
                return 1;
            }

            TaskQueue taskQueue = new TaskQueue(threads);

            for (int i = 0; i < tasks; i++) {

                taskQueue.taskEnqueue(() => Console.WriteLine($"Hello from {Thread.CurrentThread.Name}"));
            }

            taskQueue.Abort();
            taskQueue.Wait();

            return 0;
        }
        catch (Exception ex) {

            logger.Error(ex, "Stopped program because of exception");
            throw;
        }
        finally { LogManager.Shutdown(); }
            
    }
}
