using System;
using System.Collections.Generic;

namespace ExtensionGetEnumerator
{
    public static class RangeExtensions
    {
        public static IEnumerator<int> GetEnumerator(this Range range)
        {
            if (range.Start.IsFromEnd || range.End.IsFromEnd) throw new InvalidOperationException("hat operator not allowed");

            for (int i = range.Start.Value; i < range.End.Value; i++)
            {
                yield return i;
            }
        }
    }
}
