using System;
using System.Linq;

namespace RangesSample
{
    public class MyCollection
    {
        private int[] _array = Enumerable.Range(0, 100).ToArray();

        public int Length => _array.Length;

        public int[] Slice(int start, int length)
        {
            var slice = new int[length];
            Array.Copy(_array, start, slice, 0, length);
            return slice;
        }

        public int this[int index]
        {
            get => _array[index];
            set => _array[index] = value;
        }
    }
}
