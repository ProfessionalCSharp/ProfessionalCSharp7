using System;
using System.Reflection;

namespace SimpleLib
{
    public class Sample
    {
        public static string GetVersion() => $"Assembly: {Assembly.GetExecutingAssembly().FullName}";
    }
}
