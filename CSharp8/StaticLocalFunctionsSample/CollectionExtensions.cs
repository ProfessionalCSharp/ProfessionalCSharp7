using System;
using System.Collections.Generic;

namespace StaticLocalFunctionsSample
{
    static class CollectionExtensions
    {
        public static IEnumerable<T> Where1<T>(this IEnumerable<T> source, Func<T, bool> pred)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (pred == null) throw new ArgumentNullException(nameof(pred));

            return Iterator();

            IEnumerable<T> Iterator()
            {
                foreach (T item in source)
                {
                    if (pred(item))
                        yield return item;
                }
            }
        }

        public static IEnumerable<T> Where2<T>(this IEnumerable<T> source, Func<T, bool> pred)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (pred == null) throw new ArgumentNullException(nameof(pred));

            return Iterator(source, pred);

            static IEnumerable<T> Iterator(IEnumerable<T> source, Func<T, bool> pred)
            {
                foreach (T item in source)
                {
                    if (pred(item))
                        yield return item;
                }
            }
        }
    }
}
