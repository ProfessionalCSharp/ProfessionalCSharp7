using System;

namespace InParameterSample
{
    class Program
    {
        static void Main(string[] args)
        {
            AValueType vt = new AValueType { Data = 42 };
            CantChange(vt);
            int dt = 42;
            CantChange(dt);
        }

        static void CantChange(in AValueType a)
        {
            // a.Data = 43;  // does not compile - readonly variable
            Console.WriteLine(a.Data);
        }

        static void CantChange(in int x)
        {
            // x = 43; // does not compile - readonly variable
        }
    }
}
