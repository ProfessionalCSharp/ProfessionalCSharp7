using System;
using System.Buffers;
using System.Collections.Generic;

namespace ArrayPoolSample
{
    class Program
    {
        private const int ARRAYSIZE = 1000;

        static void Main()
        {
            UsingSimpleArrays();
            UsingSimpleArraysWithGC();
            UseSharedArrayPool();
            // RentArrayPool();
            // UseSharedPool();
        }

        private static void UseSharedArrayPool()
        {
            Console.WriteLine(nameof(UseSharedArrayPool));
            for (int i = 0; i < 20; i++)
            {
                LocalUseOfSharedPool(i);
            }
            Console.WriteLine();
            Console.WriteLine();
        }

        private static void UsingSimpleArraysWithGC()
        {
            Console.WriteLine(nameof(UsingSimpleArraysWithGC));
            for (int i = 0; i < 20; i++)
            {
                GC.Collect(0);
                LocalUseOfArray(i);
            }
            Console.WriteLine();
            Console.WriteLine();
        }

        private static void UsingSimpleArrays()
        {
            Console.WriteLine(nameof(UsingSimpleArrays));
            for (int i = 0; i < 20; i++)
            {
                LocalUseOfArray(i);
            }
            Console.WriteLine();
            Console.WriteLine();
        }

        private static void LocalUseOfSharedPool(int i)
        {
            int[] arr = ArrayPool<int>.Shared.Rent(ARRAYSIZE);
            ShowAddress($"simple array {i}", arr);
            FillTheArray(arr);
            UseTheArray(arr);
            ArrayPool<int>.Shared.Return(arr);
        }

        private static void LocalUseOfArray(int i)
        {
            int[] arr = new int[ARRAYSIZE];
            ShowAddress($"simple array {i}", arr);
            FillTheArray(arr);
            UseTheArray(arr);
        }

        private static void UseTheArray(int[] arr)
        {
            long max = 0;
            foreach (int item in arr)
            {
                max = max < item ? item : max;
            }
          //  Console.WriteLine($"max: {max}");
        }

        private static void FillTheArray(int[] arr)
        {
            for (int i = 0; i < ARRAYSIZE; i++)
            {
                arr[i] = i + 1;
            }
        }

        unsafe private static void ShowAddress(string name, int[] item)
        {
            fixed (int* addr = item)
            {
                Console.Write($"\t0x{(ulong)addr:X}");
            }
        }

        private static void UseSharedPool1()
        {
            ArrayPool<int> pool = ArrayPool<int>.Shared;

            var activeArrays = new List<int[]>();
            // allocate multiple arrays
            for (int i = 0; i < 10; i++)
            {
                int[] arr = pool.Rent(10);
                ShowAddress($"rent {i}", arr);
                activeArrays.Add(arr);
                for (int j = 0; j < 10; j++)
                {
                    arr[j] = i;
                }
            }

            int ix = 0;
            foreach (var arr in activeArrays)
            {
                Console.WriteLine($"{ix}.");
                Console.Write("\t");
                foreach (int x in arr)
                {
                    Console.Write(x);
                }
                Console.WriteLine();
                ix++;
            }

            foreach (var arr in activeArrays)
            {
                pool.Return(arr);
            }
        }

        private static void RentArrayPool()
        {
            ArrayPool<int> pool = ArrayPool<int>.Create(maxArrayLength: 100, maxArraysPerBucket: 10);

            var activeArrays = new List<int[]>();
            // allocate multiple arrays
            for (int i = 0; i < 10; i++)
            {
                int[] arr = pool.Rent(10);
                ShowAddress($"rent {i}", arr);
                activeArrays.Add(arr);
                for (int j = 0; j < 10; j++)
                {
                    arr[j] = i;
                }
            }

            int ix = 0;
            foreach (var arr in activeArrays)
            {
                Console.WriteLine($"{ix}.");
                Console.Write("\t");
                foreach (int x in arr)
                {
                    Console.Write(x);
                }
                Console.WriteLine();
                ix++;
            }

            foreach (var arr in activeArrays)
            {
                pool.Return(arr);
            }
        }
    }
}
