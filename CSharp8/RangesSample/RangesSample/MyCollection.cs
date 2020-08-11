using System;
using System.Linq;

namespace RangesSample
{
    public class MyCollection
    {
        private int[] _array = Enumerable.Range(0, 100).ToArray();

        public int Length
        {
            get
            {
                Console.Write("Length ");
                return _array.Length;
            }
        }

        public int[] Slice(int start, int length)
        {
            var slice = new int[length];
            Array.Copy(_array, start, slice, 0, length);
            return slice;
        }
    }
}
