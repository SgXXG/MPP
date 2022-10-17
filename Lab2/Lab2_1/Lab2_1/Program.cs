using Mutex = Lab2_1.Mutex;

var mutex = new Mutex(0);

mutex.Lock();

ThreadPool.QueueUserWorkItem(obj =>
{
    var mutex = (Mutex)obj;

    mutex.Lock();
    Console.WriteLine("I'm some thread");
    Thread.Sleep(1000);
    Console.WriteLine("Waiting is over");
    mutex.Unlock();

}, mutex);

Console.WriteLine("I'm main proc");
Thread.Sleep(1000);
Console.WriteLine("Main waiting is over");
mutex.Unlock();

//Console.ReadLine();