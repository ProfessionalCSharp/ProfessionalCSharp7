using System;
using System.Reflection;
using System.Text;

namespace TypeView
{
    class Program
    {
        private static StringBuilder OutputText = new StringBuilder();

        static void Main()
        {
            // modify this line to retrieve details of any other data type
            Type t = typeof(double);

            AnalyzeType(t);
            Console.WriteLine($"Analysis of type {t.Name}");
            Console.WriteLine(OutputText.ToString());

            Console.ReadLine();
        }

        static void AnalyzeType(Type t)
        {
            TypeInfo typeInfo = t.GetTypeInfo();
            AddToOutput($"Type Name: {t.Name}");
            AddToOutput($"Full Name: {t.FullName}");
            AddToOutput($"Namespace: {t.Namespace}");

            Type tBase = typeInfo.BaseType;

            if (tBase != null)
            {
                AddToOutput($"Base Type: {tBase.Name}");
            }

            AddToOutput("\npublic members:");
            foreach (MemberInfo member in t.GetMembers())
            {
                AddToOutput($"{member.DeclaringType} {member.MemberType} {member.Name}");
            }
        }

        static void AddToOutput(string Text) =>
            OutputText.Append($"{Environment.NewLine} {Text}");
    }
}
