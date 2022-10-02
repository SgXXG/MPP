using System;
using System.Threading;

namespace Lab2_1
{
    class Mutex
    {
        private int _holderId = -1;

        public void Lock()
        {
            var id = Thread.CurrentThread.ManagedThreadId;
            while (Interlocked.CompareExchange(ref _holderId, id, -1) != -1)
            {
                Thread.Sleep(10);
            }
        }

        public void Unlock()
        {
            var id = Thread.CurrentThread.ManagedThreadId;
            Interlocked.CompareExchange(ref _holderId, -1, id);
        }
    }

    static class Program
    {
        static void Main(string[] args)
        {
            var mutex = new Mutex();
            for (var i = 0; i < 10; i++)
            {
                new Thread(() =>
                {
                    mutex.Lock();
                    Console.WriteLine("Thread #" + Thread.CurrentThread.ManagedThreadId + " locked mutex.");
                    Thread.Sleep(400);
                    Console.WriteLine("Thread #" + Thread.CurrentThread.ManagedThreadId + " unlocked mutex.");
                    mutex.Unlock();
                }).Start();
            }

            Console.ReadLine();
        }
    }
}