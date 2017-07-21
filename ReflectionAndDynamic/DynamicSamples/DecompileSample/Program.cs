using System;

namespace DecompileSample
{
    class Program
    {
        static void Main()
        {
            // StaticClass staticObject = new StaticClass();
            DynamicClass dynamicObject = new DynamicClass();
            // Console.WriteLine(staticObject.IntValue);
            Console.WriteLine(dynamicObject.DynValue);
            Console.ReadLine();
        }
    }

    class StaticClass
    {
        public int IntValue = 100;
    }
    class DynamicClass
    {
        public dynamic DynValue = 100;
    }

}
