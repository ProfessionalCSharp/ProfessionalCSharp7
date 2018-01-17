namespace RefStructSample
{
    class Program
    {
        static void Main(string[] args)
        {
            ValueTypeOnly vt = new ValueTypeOnly(42);
            vt.AMethod();
           // vt.ToString is not allowed! 
        }
    }
}
