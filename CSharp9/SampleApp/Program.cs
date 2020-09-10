using System;
using HelloWorldGenerated;
using Microsoft.CodeAnalysis;
using ShowSyntaxTrees;

namespace SampleApp
{
    class Program
    {
        static void Main()
        {
            
            HelloWorld.SayHello();
            SyntaxTrees.Show();
        }
    }
}
