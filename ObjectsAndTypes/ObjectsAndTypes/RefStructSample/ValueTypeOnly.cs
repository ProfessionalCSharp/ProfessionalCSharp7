using System;
using System.Collections.Generic;
using System.Text;

namespace RefStructSample
{
    ref struct ValueTypeOnly
    {
        public ValueTypeOnly(int y)
        {
            Y = y;
            X = 0;
        }
        public int X;
        public int Y { get; }

        public void AMethod()
        {
            Console.WriteLine($"x: {X}, y: {Y}");
        }
    }
}
