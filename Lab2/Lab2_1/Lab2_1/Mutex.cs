namespace Lab2_1
{
    public class Mutex
    {
        int value;

        public Mutex(int value)
        {
            this.value = value;
        } 

        public void Lock()
        {
            while(Interlocked.CompareExchange(ref value, 1, 0) == 1)
            {
                
            }
        }

        public void Unlock()
        {
            Interlocked.Exchange(ref value, 0);
        }
    }
}
