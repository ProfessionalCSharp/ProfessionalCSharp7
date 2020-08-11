using System;
using System.Collections.Generic;
using System.Linq;

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

        // extension methods not (yet?) possible
        //public static IList<T> Slice<T>(this IList<T> list, int start, int length)
        //{
        //    return list.Skip(start).Take(length).ToList();
        //}
    }
}
