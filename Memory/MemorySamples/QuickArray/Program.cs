using System;

namespace QuickArray
{
    public class Program
    {
        unsafe public static void Main()
        {
            Console.Write($"How big an array do you want? {Environment.NewLine}>");
            string userInput = Console.ReadLine();
            int size = int.Parse(userInput);

            long* pArray = stackalloc long[size];
            for (int i = 0; i < size; i++)
            {
                pArray[i] = i * i;
            }

            for (int i = 0; i < size; i++)
            {
                Console.WriteLine($"Element {i} = {*(pArray + i)}");
            }

            Console.ReadLine();
        }
    }
}
