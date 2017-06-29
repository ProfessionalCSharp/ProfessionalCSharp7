using System;

namespace SpanWithStrings
{
    class Program
    {
        const string text =
            "Professional C# 6 and .NET Core 1.0 provides complete coverage " +
            "of the latest updates, features, and capabilities, giving you " +
            "everything you need for C#. Get expert instruction on the latest " +
            "changes to Visual Studio 2015, Windows Runtime, ADO.NET, ASP.NET, " +
            "Windows Store Apps, Windows Workflow Foundation, and more, with " +
            "clear explanations, no-nonsense pacing, and valuable expert insight. " +
            "This incredibly useful guide serves as both tutorial and desk " +
            "reference, providing a professional-level review of C# architecture " +
            "and its application in a number of areas. You'll gain a solid " +
            "background in managed code and .NET constructs within the context of " +
            "the 2015 release, so you can get acclimated quickly and get back to work.";

        static void Main()
        {
            int ix = text.IndexOf("Visual");
            ReadOnlySpan<char> spanToText = text.AsSpan();
            ReadOnlySpan<char> slice = spanToText.Slice(ix, 13);

            string newString = new string(slice.ToArray());
            Console.WriteLine(newString);
        }
    }
}
