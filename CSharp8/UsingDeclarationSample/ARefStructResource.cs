using System;

namespace UsingDeclarationSample
{
    public ref struct ARefStructResource 
    {
        public void Foo() => Console.WriteLine("Foo");
        public void Dispose()
        {
            Console.WriteLine($"ARefStructResource:Dipose");
        }
    }
}
