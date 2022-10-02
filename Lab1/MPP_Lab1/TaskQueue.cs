namespace MPP_Lab1
{   
    public class TaskQueue
    {
        public delegate void delegateTask();
        public List<Thread> threads = new List<Thread>();
        public Queue<delegateTask> tasks = new Queue<delegateTask>();
        private object locker = new object();

        public TaskQueue(int threadsNumber)
        {
            for (int i = 0; i < threadsNumber; i++)
            {
                Thread thread = new Thread(threadTask);
                thread.IsBackground = true;
                thread.Name = "Thread"+(i+1).ToString();
                threads.Add(thread);
                thread.Start();
            }
        }
 
        public void taskEnqueue(delegateTask task)
        {
            tasks.Enqueue(task);    
        }

        public void threadTask()
        {
            while (Thread.CurrentThread.IsBackground)
            {
                if (tasks.Count != 0)
                {
                    lock (locker)
                    {
                        if (tasks.Count != 0)
                        {
                            delegateTask delTask = tasks.Dequeue();
                            if (delTask != null)
                            {
                                delTask();
                                Thread.Sleep(200);
                            }
                        }                      
                    }              
                }
            }
        }

        public void Abort()
        {
            while(tasks.Count > 0)
            {
                
            }
            foreach (Thread thread in threads)
                thread.IsBackground = false;
        }

        public void Wait()
        {
            foreach (Thread thread in threads)
                thread.Join();
        }
    }
}
