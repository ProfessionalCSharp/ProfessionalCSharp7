using System;
using System.Runtime.InteropServices;

namespace SpanSample
{

    struct CombineSpans
    {
        private Span<int> span1;
        private Span<int> span2;
    }

    class Program
    {
        static void Main(string[] args)
        {
            SpanOnTheHeap();
            SpanOnTheStack();
            SpanOnNativeMemory();
            //DangerousSpan();
            //SpanWithRef();
        }

        private static void SpanWithRef()
        {
            CombineSpans os;
            int hash = os.GetHashCode();
        }

        private static void DangerousSpan()
        {
            Console.WriteLine(nameof(DangerousSpan));
            int[] data = { 1, 2, 3, 4, 5, 6 };

            Span<int> s1 = Span<int>.DangerousCreate(data, ref data[1], 3);
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine($"s1[{i}]: {s1[i]}");
            }

            Span<int> s2 = s1.Slice(2);
            ref int refToS2 = ref s2.DangerousGetPinnableReference();
            Console.WriteLine($"dangerous: {refToS2}");

            Span<int> s3 = Span<int>.DangerousCreate(data, ref refToS2, 3);
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine($"s3[{i}]: {s3[i]}");
            }

            Console.WriteLine();
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
