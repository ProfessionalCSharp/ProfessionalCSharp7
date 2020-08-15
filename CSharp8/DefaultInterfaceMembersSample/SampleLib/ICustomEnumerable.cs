using System;
using System.Collections.Generic;

namespace SampleLib
{
    public interface ICustomEnumerable<T> : IEnumerable<T>
    {
        public IEnumerable<T> Where(Func<T, bool> pred)
        {
            foreach (T item in this)
            {
                if (pred(item))
                {
                    yield return item;
                }
            }
        }
    }
}
