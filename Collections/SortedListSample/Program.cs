using System.Collections.Generic;
using System;

namespace Wrox.ProCSharp.Collections
{
    class Program
    {
        static void Main()
        {
            var books = new SortedList<string, string>();
            books.Add("Professional WPF Programming", "978–0–470–04180–2");
            books.Add("Professional ASP.NET MVC 5", "978–1–118-79475–3");

            books["Beginning Visual C# 2012 Programming"] = "978–1–118-31441-8";
            books["Professional C# 5.0 and .NET 4.5.1"] = "978–1–118–83303–2";

            foreach (KeyValuePair<string, string> book in books)
            {
                Console.WriteLine($"{book.Key}, {book.Value}");
            }

            foreach (string isbn in books.Values)
            {
                Console.WriteLine(isbn);
            }

            foreach (string title in books.Keys)
            {
                Console.WriteLine(title);
            }

            {
                string title = "Professional C# 7.0";
                if (!books.TryGetValue(title, out string isbn))
                {
                    Console.WriteLine($"{title} not found");
                }
            }
        }
    }
}
