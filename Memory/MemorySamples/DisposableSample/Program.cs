namespace DisposableSample
{
    class Program
    {
        static void Main()
        {
            using (var resource = new SomeResource())
            {
                resource.Foo();
            }
        }
    }
}
