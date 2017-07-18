using System;
using System.Runtime.CompilerServices;

namespace CallerInformation
{
    class Program
    {
        static void Main()
        {
            var p = new Program();
            p.Log();
            p.SomeProperty = 33;
            Action a1 = () => p.Log();
            a1();
        }

        private int _someProperty;
        public int SomeProperty
        {
            get => _someProperty;
            set
            {
                Log();
                _someProperty = value;
            }
        }

        public void Log([CallerLineNumber] int line = -1,
            [CallerFilePath] string path = null,
            [CallerMemberName] string name = null)
        {
            Console.WriteLine((line < 0) ? "No line" : "Line " + line);
            Console.WriteLine((path == null) ? "No file path" : path);
            Console.WriteLine((name == null) ? "No member name" : name);
            Console.WriteLine();
        }
    }
}
