using System;

namespace SpanSample
{
    class Program
    {
        static void Main()
        {
            Span<int> span1 = IntroSpans();
            Span<int> span2 = CreateSlices(span1);
            ChangeValues(span1, span2);
            ReadonlySpan(span1);
        }

        private static void ReadonlySpan(Span<int> span1)
        {
            Console.WriteLine(nameof(ReadonlySpan));
            int[] arr = span1.ToArray();
            ReadOnlySpan<int> readOnlySpan1 = new ReadOnlySpan<int>(arr);
            DisplaySpan("readOnlySpan1", readOnlySpan1);

            ReadOnlySpan<int> readOnlySpan2 = span1;
            DisplaySpan("readOnlySpan2", readOnlySpan2);
            ReadOnlySpan<int> readOnlySpan3 = arr;
            DisplaySpan("readOnlySpan3", readOnlySpan3);
            Console.WriteLine();
        }

        private static void ChangeValues(Span<int> span1, Span<int> span2)
        {
            Console.WriteLine(nameof(ChangeValues));
            Span<int> span4 = span1.Slice(start: 4);
            span4.Clear();
            DisplaySpan("content of span1", span1);
            Span<int> span5 = span2.Slice(start: 3, length: 3);
            span5.Fill(42);
            DisplaySpan("content of span2", span2);
            span5.CopyTo(span1);
            DisplaySpan("content of span1", span1);

            if (!span1.TryCopyTo(span4))
            {
                Console.WriteLine("Couldn't copy span1 to span4 because span4 is too small");
                Console.WriteLine($"length of span4: {span4.Length}, length of span1: {span1.Length}");
            }
            Console.WriteLine();
        }

        private static Span<int> CreateSlices(Span<int> span1)
        {
            Console.WriteLine(nameof(CreateSlices));
            int[] arr2 = { 3, 5, 7, 9, 11, 13, 15 };
            var span2 = new Span<int>(arr2);
            var span3 = new Span<int>(arr2, start: 3, length: 3);
            var span4 = span1.Slice(start: 2, length: 4);

            DisplaySpan("content of span3", span3);
            DisplaySpan("content of span4", span4);
            Console.WriteLine();
            return span2;
        }

        private static void DisplaySpan(string title, ReadOnlySpan<int> span)
        {
            Console.WriteLine(title);
            for (int i = 0; i < span.Length; i++)
            {
                Console.Write($"{span[i]}.");
            }
            Console.WriteLine();
        }

        private static Span<int> IntroSpans()
        {
            int[] arr1 = { 2, 4, 6, 8, 10, 12 };
            var span1 = new Span<int>(arr1);
            span1[1] = 11;
            Console.WriteLine($"arr1[1] is changed via span1[1]: {arr1[1]}");
            Console.WriteLine();
            return span1;
        }
    }
}
