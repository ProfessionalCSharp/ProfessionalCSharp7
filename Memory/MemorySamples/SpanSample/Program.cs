using System;
using System.Runtime.InteropServices;

namespace SpanSample
{
    class Program
    {
        static void Main(string[] args)
        {
            SpanOnTheHeap();
            SpanOnTheStack();
            SpanOnNativeMemory();
        }

        private static unsafe void SpanOnNativeMemory()
        {
            Console.WriteLine(nameof(SpanOnNativeMemory));
            const int nbytes = 100;
            IntPtr p = Marshal.AllocHGlobal(nbytes);
            try
            {              
                int* p2 = (int*)p.ToPointer();
                
                Span<int> span = new Span<int>(p2, nbytes >> 2);
                span.Fill(42);

                int max = nbytes >> 2;
                for (int i = 0; i < max; i++)
                {
                    Console.Write($"{span[i]} ");
                }
            }
            finally
            {
                Marshal.FreeHGlobal(p);
            }
            Console.WriteLine();
        }

        private static unsafe void SpanOnTheStack()
        {
            Console.WriteLine(nameof(SpanOnTheStack));

            long* lp = stackalloc long[20];
            var span1 = new Span<long>(lp, 20);
            
            for (int i = 0; i < 20; i++)
            {
                span1[i] = i;
            }

            Console.WriteLine(string.Join(", ", span1.ToArray()));
            Console.WriteLine();
        }

        private static void SpanOnTheHeap()
        {
            Console.WriteLine(nameof(SpanOnTheHeap));
            Span<int> span1 = (new int[] { 1, 5, 11, 71, 22, 19, 21, 33 }).AsSpan();

            span1.Slice(start: 4, length: 3).Fill(42);

            Console.WriteLine(string.Join(", ", span1.ToArray()));

            Console.WriteLine();
        }
    }
}
