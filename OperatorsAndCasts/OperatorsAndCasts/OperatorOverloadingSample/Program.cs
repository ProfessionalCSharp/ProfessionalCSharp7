using System;

namespace OperatorOverloadingSample
{
    public class Program
    {
        public static void Main()
        {
            Vector vect1, vect2, vect3;

            vect1 = new Vector(3.0, 3.0, 1.0);
            vect2 = new Vector(2.0, -4.0, -4.0);
            vect3 = vect1 + vect2;

            Console.WriteLine($"vect1 = {vect1}");
            Console.WriteLine($"vect2 = {vect2}");
            Console.WriteLine($"vect3 = {vect3}");
        }
    }
}
