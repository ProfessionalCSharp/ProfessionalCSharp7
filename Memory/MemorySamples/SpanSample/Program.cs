using System;
using System.Runtime.InteropServices;

namespace SpanSample
{
    struct OuterSpan
    {
        private Span<int> innerSpan;
    }

    class Program
    {
        static void Main(string[] args)
        {
            SpanOnTheHeap();
            SpanOnTheStack();
            SpanOnNativeMemory();
            DangerousSpan();
            SpanWithRef();
        }

        private static void SpanWithRef()
        {
            OuterSpan os;
            os.ToString();
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
            const int nbytes = 1000;
            IntPtr p = Marshal.AllocHGlobal(nbytes);
            try
            {

                int* p2 = (int*)p.ToPointer();

                Span<int> span = new Span<int>(p2, nbytes >> 2);
                span.Fill(42);

                int max = 1000 >> 2;
                for (int i = 0; i < max; i++)
                {
                    Console.Write(span[i]);
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
            
            Span<long> slice1 = span1.Slice(0, 20);
            for (int i = 0; i < 20; i++)
            {
                slice1[i] = i;
            }

            foreach (long item in span1.ToArray())
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();
        }

        private static void SpanOnTheHeap()
        {
            Console.WriteLine(nameof(SpanOnTheHeap));
            var span1 = new Span<int>(new int[] { 1, 5, 11, 71, 22, 19, 21, 33 });

            span1.Slice(2, 4).Fill(42);

            foreach (int i in span1.ToArray())
            {
                Console.WriteLine(i);
            }
            Console.WriteLine();
        }
    }
}
