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

            books["Beginning C# 6 Programming"] = "978-1-119-09668-9";
            books["Professional C# 6 and .NET Core 1.0"] = "978-1-119-09660-3";

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
                string title = "Professional C# 8";
                if (!books.TryGetValue(title, out string isbn))
                {
                    Console.WriteLine($"{title} not found");
                }
            }
        }
    }
}
