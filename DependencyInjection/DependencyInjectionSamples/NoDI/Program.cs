using System;

namespace NoDI
{
    class Program
    {
        static void Main()
        {
            var controller = new HomeController();
            string result = controller.Hello("Stephanie");
            Console.WriteLine(result);
        }
    }
}
