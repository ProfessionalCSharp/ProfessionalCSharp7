using System;
using System.Collections;
using System.Collections.Generic;

namespace Wrox.ProCSharp.Arrays
{
    class HelloCollection
    {
        public IEnumerator<string> GetEnumerator()
        {
            yield return "Hello";
            yield return "World";
        }
    }

    class Program
    {
        static void Main()
        {
            HelloWorld();
            MusicTitles();

            var game = new GameMoves();

            IEnumerator enumerator = game.Cross();
            while (enumerator.MoveNext())
            {
                enumerator = enumerator.Current as IEnumerator;
            }
        }

        static void MusicTitles()
        {
            var titles = new MusicTitles();
            foreach (var title in titles)
            {
                Console.WriteLine(title);
            }
            Console.WriteLine();

            Console.WriteLine("reverse");
            foreach (var title in titles.Reverse())
            {
                Console.WriteLine(title);
            }
            Console.WriteLine();

            Console.WriteLine("subset");
            foreach (var title in titles.Subset(2, 2))
            {
                Console.WriteLine(title);
            }
        }

        static void HelloWorld()
        {
            var helloCollection = new HelloCollection();
            foreach (string s in helloCollection)
            {
                Console.WriteLine(s);
            }
        }
    }
}
