using System;
using System.Buffers;

namespace ArrayPoolSample
{
    class Program
    {
        static void Main()
        {
            for (int i = 0; i < 10; i++)
            {
                int arrayLength = (i + 1) << 10;
                int[] arr = ArrayPool<int>.Shared.Rent(arrayLength);
                Console.WriteLine($"requested an array of {arrayLength} and received {arr.Length}");
                for (int j = 0; j < arrayLength * j; j++)
                {
                    arr[j] = j;
                }
                ArrayPool<int>.Shared.Return(arr, clearArray: true);
            }
        }     
    }
}
