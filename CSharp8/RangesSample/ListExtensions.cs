using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace RangesSample
{
    public static class ListExtensions
    {
        public static IList<T> Range<T>(this IList<T> list, Range range)
        {
            var count = list.Count;
            var start = range.Start.IsFromEnd ? count - range.Start.Value : range.Start.Value;
            var end = range.End.IsFromEnd ? count - range.End.Value : range.End.Value;
            if (start > end) (start, end) = (end, start);
            var rng = Enumerable.Range(start, end - start);
            return list.Where((item, index) => rng.Any(x => x == index)).ToList();
        }

        public static IEnumerable<T> Slice<T>(this IList<T> list, Range range)
        {
            (var offset, var length) = range.GetOffsetAndLength(list.Count);
            //var indexRange = Enumerable.Range(offset, length);
            //return list.Where((item, index) => indexRange.Any(x => x == index)).ToList();
            return list.Skip(offset).Take(length).ToList();
        }
    }
}
