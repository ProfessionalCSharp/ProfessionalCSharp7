using System;

namespace EqualsSample
{
    class Program
    {
        static void Main()
        {
            ReferenceEqualsSample();
        }

        private static void ReferenceEqualsSample()
        {
            SomeClass x = new SomeClass(), y = new SomeClass(), z = x;

            bool b1 = object.ReferenceEquals(null, null); // returns true
            bool b2 = object.ReferenceEquals(null, x);    // returns false
            bool b3 = object.ReferenceEquals(x, y);       // returns false because x and y
                                                          // reference different objects
            bool b4 = object.ReferenceEquals(x, z);       // references the same object
        }
    }
}
