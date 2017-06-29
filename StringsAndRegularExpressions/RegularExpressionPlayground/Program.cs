using System;
using System.Text.RegularExpressions;

namespace RegularExpressionPlayground
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
            Find1(text);
            Find2(text);
            Groups();
            NamedGroups();
            Console.ReadLine();
        }

        public static void NamedGroups()
        {
            Console.WriteLine("NamedGroups\n");
            string line = "Hey, I've just found this amazing URI at http:// what was it --oh yes https://www.wrox.com or http://www.wrox.com:80";

            string pattern = @"\b(?<protocol>https?)(?:://)(?<address>[.\w]+)([\s:](?<port>[\d]{2,4})?)\b";
            Regex r = new Regex(pattern, RegexOptions.ExplicitCapture);

            MatchCollection mc = r.Matches(line);
            foreach (Match m in mc)
            {
                Console.WriteLine($"match: {m} at {m.Index}");

                foreach (var groupName in r.GetGroupNames())
                {
                    Console.WriteLine($"match for {groupName}: {m.Groups[groupName].Value}");
                }
                Console.WriteLine();
            }
            Console.WriteLine();

        }

        public static void Groups()
        {
            Console.WriteLine("Groups\n");
            string line = "Hey, I've just found this amazing URI at http:// what was it --oh yes https://www.wrox.com or http://www.wrox.com:80";

            string pattern = @"\b(https?)(://)([.\w]+)([\s:]([\d]{2,4})?)\b";
            var r = new Regex(pattern);
            MatchCollection mc = r.Matches(line);            
            
            foreach (Match m in mc)
            {
                Console.WriteLine($"Match: {m}\n");
                foreach (Group g in m.Groups)
                {
                    if (g.Success)
                    {
                        Console.WriteLine($"group index: {g.Index}, value: {g.Value}");
                    }
                }
                Console.WriteLine();             
            }
            Console.WriteLine();
        }

        public static void Find1(string text)
        {
            Console.WriteLine("Find1\n");
            const string pattern = "ion";

            MatchCollection matches = Regex.Matches(text, pattern,
                                                    RegexOptions.IgnoreCase |
                                                    RegexOptions.ExplicitCapture);
            WriteMatches(text, matches);
            Console.WriteLine();
        }

        public static void WriteMatches(string text, MatchCollection matches)
        {
            Console.WriteLine($"Original text was: \n\n{text}\n");
            Console.WriteLine($"No. of matches: {matches.Count}");

            foreach (Match nextMatch in matches)
            {
                int index = nextMatch.Index;
                string result = nextMatch.ToString();
                int charsBefore = (index < 5) ? index : 5;
                int fromEnd = text.Length - index - result.Length;
                int charsAfter = (fromEnd < 5) ? fromEnd : 5;
                int charsToDisplay = charsBefore + charsAfter + result.Length;

                Console.WriteLine($"Index: {index}, \tString: {result}, \t" +
                  $"{text.Substring(index - charsBefore, charsToDisplay)}");
            }
        }

        public static void Find2(string text)
        {
            Console.WriteLine("Find2\n");
            const string pattern = @"\ba\S*ure\b";
            MatchCollection matches = Regex.Matches(text, pattern,
                RegexOptions.IgnoreCase);
            WriteMatches(text, matches);
            Console.WriteLine();
        }
    }
}
