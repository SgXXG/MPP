using System;
using System.Threading;

namespace MPP_Lab_4_1
{
    delegate void ParallelDelegate();

    static class Parallel
    {
        class Int32Wrapper
        {
            public Int32Wrapper(int value)
            {
                Value = value;
            }

            public int Value;
        }

        public static void WaitAll(ParallelDelegate[] delegates)
        {
            var resetEvent = new ManualResetEvent(false);
            var threadsCountWrapper = new Int32Wrapper(delegates.Length);
            foreach (var parallelDelegate in delegates)
            {
                var info = new Tuple<ParallelDelegate, Int32Wrapper, ManualResetEvent>(parallelDelegate, threadsCountWrapper, resetEvent);
                ThreadPool.QueueUserWorkItem(ParallelDelegateWrapper, info);
            }
            resetEvent.WaitOne();
        }

        private static void ParallelDelegateWrapper(object info)
        {
            var tupleInfo = info as Tuple<ParallelDelegate, Int32Wrapper, ManualResetEvent>;
            tupleInfo.Item1();
            if (Interlocked.Decrement(ref tupleInfo.Item2.Value) == 0)
            {
                tupleInfo.Item3.Set();
            }
        }
    }

    static class Program
    {
        private static void Sleep3()
        {
            Console.WriteLine("Sleep3: started.");
            Thread.Sleep(3000);
            Console.WriteLine("Sleep3: finished.");
        }

        private static void Sleep5()
        {
            Console.WriteLine("Sleep5: started.");
            Thread.Sleep(5000);
            Console.WriteLine("Sleep5: finished.");
        }
        private static void Sleep7()
        {
            Console.WriteLine("Sleep7: started.");
            Thread.Sleep(7000);
            Console.WriteLine("Sleep7: finished.");
        }

        static void Main(string[] args)
        {
            Parallel.WaitAll(new ParallelDelegate[] { Sleep3, Sleep5, Sleep7 });
            Console.WriteLine("Program: finished.");
        }
    }

}