using System;
using System.Runtime.InteropServices;

namespace Lab2_2 {

    class OsHandle : IDisposable {
        
        private int handle;
        private bool disposed = false;

        public OsHandle(int handle) {
            
            this.handle = handle;
        }

        public int Handle {

            set { handle = value; }
            get {
                
                if (disposed) {
                    
                    throw new ObjectDisposedException(nameof(handle));
                }
                
                return this.handle;
            }
        }

        ~OsHandle() {
            
            Dispose(false);
        }

        public void Dispose() {
           
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) {
            
            if (disposed) return;

            if (disposing) { ReleaseManageResourses(); }

            ReleaseUnmanageResourses(Handle);
            disposed = true;
        }

        protected void ReleaseUnmanageResourses(int handle) {
           
            ReleaseHandle(handle);
        }

        protected void ReleaseHandle(int handle) {
           
            Console.WriteLine($"Handle {handle} is released");
        }

        protected void ReleaseManageResourses() {
          
            Console.WriteLine("Manage resourses has been released");
        }
    }

    static class Program {

        static void Main(string[] args) {
            
            var osHandle = new OsHandle(100);

            // work with handle
            Console.WriteLine("Working with osHandle...");
            //Release resourses
            GC.Collect();
            osHandle.Dispose();
        }
    }
}