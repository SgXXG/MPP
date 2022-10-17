namespace Lab2_2
{
    public class OSHAndle: IDisposable
    {

        private int handle;
        private bool disposed = false;

        public OSHAndle(int handle)
        {
            this.handle = handle;
        }

        public int Handle { 
            set { handle = value; }  
            get {
                if (disposed)
                {
                    throw new ObjectDisposedException(nameof(handle));
                }
                return this.handle;
            } 
        }

        ~OSHAndle()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed) 
                return;

            if (disposing)
            {
                ReleaseManageResourses();
            }

            ReleaseUnmanageResourses(Handle);
            disposed = true;
        }

        protected void ReleaseUnmanageResourses(int handle)
        {
            ReleaseHandle(handle);
        }

        protected void ReleaseHandle(int handle)
        {
            Console.WriteLine($"Handle {handle} is released");
        }

        protected void ReleaseManageResourses()
        {
            Console.WriteLine("Manage resourses has been released");
        }
    }
}
