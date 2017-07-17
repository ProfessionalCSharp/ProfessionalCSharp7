using System;

namespace LocalFunctions
{
    public static class Algorithms
    {
        public static void QuickSort<T>(T[] elements) where T : IComparable<T>
        {
            void Sort(int start, int end)
            {
                int i = start, j = end;
                var pivot = elements[(start + end) / 2];

                while (i <= j)
                {
                    while (elements[i].CompareTo(pivot) < 0) i++;
                    while (elements[j].CompareTo(pivot) > 0) j--;
                    if (i <= j)
                    {
                        T tmp = elements[i];
                        elements[i] = elements[j];
                        elements[j] = tmp;
                        i++;
                        j--;
                    }
                }
                if (start < j) Sort(start, j);
                if (i < end) Sort(i, end);
            }

            Sort(0, elements.Length - 1);
        }

        public static void WhenDoesItEnd()
        {
            Console.WriteLine(nameof(WhenDoesItEnd));
            void InnerLoop(int ix)
            {
                Console.WriteLine(ix++);
                InnerLoop(ix);
            }
            InnerLoop(1);
        }
    }
}
