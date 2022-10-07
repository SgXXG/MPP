using System;
using System.Threading;

namespace Lab2_1 {
    
    class Mutex {
        
        int value;

        public Mutex(int value) {
            
            this.value = value;
        }

        public void Lock() {
            
            while (Interlocked.CompareExchange(ref value, 1, 0) == 1) { }
        }

        public void Unlock() {
            
            Interlocked.Exchange(ref value, 0);
        }
    }

    static class Program {
        
        static void Main(string[] args) {
          
            var mutex = new Mutex(0);

            for (var i = 0; i < 10; i++) {
               
                new Thread(() => {
                    
                    mutex.Lock();
                    Console.WriteLine("Thread #" + Thread.CurrentThread.ManagedThreadId + " locked mutex.");
                    Thread.Sleep(400);
                    Console.WriteLine("Thread #" + Thread.CurrentThread.ManagedThreadId + " unlocked mutex.");
                    mutex.Unlock();
                }).Start();
            }
        }
    }
}