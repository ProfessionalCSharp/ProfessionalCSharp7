using System;

namespace WithDI
{
    class Program
    {
        static void Main()
        {
            var controller = new HomeController(new GreetingService());
            string result = controller.Hello("Matthias");
            Console.WriteLine(result);
        }
    }
}
