using System;
using System.Collections.Generic;

namespace Wrox.ProCSharp.Delegates
{
    class BubbleSorter
    {
        static public void Sort<T>(IList<T> sortArray, Func<T, T, bool> comparison)
        {
            bool swapped = true;
            do
            {
                swapped = false;
                for (int i = 0; i < sortArray.Count - 1; i++)
                {
                    if (comparison(sortArray[i + 1], sortArray[i]))
                    {
                        T temp = sortArray[i];
                        sortArray[i] = sortArray[i + 1];
                        sortArray[i + 1] = temp;
                        swapped = true;
                    }
                }
            } while (swapped);
        }
    }
}
