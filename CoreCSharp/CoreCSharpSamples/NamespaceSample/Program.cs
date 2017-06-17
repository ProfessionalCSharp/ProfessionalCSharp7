using System;
using Introduction = Wrox.ProCSharp.Basics;

class Program
{
    static void Main()
    {
        Introduction::NamespaceExample NSEx = new Introduction::NamespaceExample();
        Console.WriteLine(NSEx.GetNamespace());
    }
}

namespace Wrox.ProCSharp.Basics
{
    class NamespaceExample
    {
        public string GetNamespace() => GetType().Namespace;
    }
}