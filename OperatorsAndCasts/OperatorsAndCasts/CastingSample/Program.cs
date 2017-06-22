using System;

namespace CastingSample
{
    class Program
    {
        static void Main()
        {
            try
            {
                var balance = new Currency(50, 35);

                Console.WriteLine(balance);
                Console.WriteLine($"balance is {balance}");

                float balance2 = balance;

                Console.WriteLine($"After converting to float, = {balance2}");

                balance = (Currency)balance2;

                Console.WriteLine($"After converting back to Currency, = {balance}");
                Console.WriteLine("Now attempt to convert out of range value of " +
                                    "-$50.50 to a Currency:");

                // Overflow Exception
                //checked
                //{
                //    balance = (Currency)(-50.50);
                //    WriteLine($"Result is {balance}");
                //}

                uint balance3 = (uint)balance2;

                Console.WriteLine($"Converting to uint gives {balance3}");

            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception occurred: {e.Message}");
            }
        }
    }
}