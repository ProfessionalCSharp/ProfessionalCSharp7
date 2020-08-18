using System;

namespace UsingDeclarationSample
{
    class Program
    {
        static void Main(string[] args)
        {
            UsingStatement();
            UsingDeclaration();

            // pattern based using with ref struct
            using var y = new ARefStructResource();
        }

        private static void UsingDeclaration()
        {
            using var r = new AResource();
            r.Use();
        }

        private static void UsingStatement()
        {
            using (var r = new AResource())
            {
                r.Use();
            }
        }
    }
}
