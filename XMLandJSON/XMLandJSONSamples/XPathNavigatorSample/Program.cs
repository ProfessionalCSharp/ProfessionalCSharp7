using System;
using System.IO;
using System.Xml;
using System.Xml.XPath;

namespace XPathNavigatorSample
{
    class Program
    {
        private const string BooksFileName = "books.xml";
        private const string NewBooksFileName = "newbooks.xml";
        private const string NavigateOption = "-n";
        private const string EvaluateOption = "-e";
        private const string ChangeOption = "-c";

        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                ShowUsage();
                return;
            }

            switch (args[0])
            {
                case NavigateOption:
                    SimpleNavigate();
                    break;
                case EvaluateOption:
                    UseEvaluate();
                    break;
                case ChangeOption:
                    Insert();
                    break;
                default:
                    ShowUsage();
                    break;
            }
        }

        private static void ShowUsage()
        {
            Console.WriteLine("XmlReaderAndWriterSample options");
            Console.WriteLine("\tOptions");
            Console.WriteLine($"\t{NavigateOption}\tNavigate");
            Console.WriteLine($"\t{EvaluateOption}\tEvaluate");
            Console.WriteLine($"\t{ChangeOption}\tChange");
        }

        public static void SimpleNavigate()
        {
            //modify to match your path structure
            var doc = new XPathDocument(BooksFileName);
            //create the XPath navigator
            XPathNavigator nav = doc.CreateNavigator();
            //create the XPathNodeIterator of book nodes
            // that have genre attribute value of novel
            XPathNodeIterator iterator = nav.Select("/bookstore/book[@genre='novel']");

            while (iterator.MoveNext())
            {
                XPathNodeIterator newIterator = iterator.Current.SelectDescendants(XPathNodeType.Element, matchSelf: false);
                while (newIterator.MoveNext())
                {
                    Console.WriteLine($"{newIterator.Current.Name}: {newIterator.Current.Value}");
                }
            }
        }

        public static void UseEvaluate()
        {
            //modify to match your path structure
            var doc = new XPathDocument(BooksFileName);

            //create the XPath navigator
            XPathNavigator nav = doc.CreateNavigator();

            //create the XPathNodeIterator of book nodes
            XPathNodeIterator iterator = nav.Select("/bookstore/book");
            while (iterator.MoveNext())
            {
                if (iterator.Current.MoveToChild("title", string.Empty))
                {
                    Console.WriteLine($"{iterator.Current.Name}: {iterator.Current.Value}");
                }
            }
            Console.WriteLine("=========================");
            Console.WriteLine($"Total Cost = {nav.Evaluate("sum(/bookstore/book/price)")}");
        }

        public static void Insert()
        {
            var doc = new XmlDocument();
            doc.Load(BooksFileName);
            // var doc = new XPathDocument(BooksFileName);

            XPathNavigator navigator = doc.CreateNavigator();

            if (navigator.CanEdit)
            {
                Console.WriteLine($"edit {NewBooksFileName} and add <disc>");
                XPathNodeIterator iter = navigator.Select("/bookstore/book/price");

                while (iter.MoveNext())
                {
                    iter.Current.InsertAfter("<disc>5</disc>");
                }
            }          

            using (var stream = File.CreateText(NewBooksFileName))
            {
                var outDoc = new XmlDocument();
                outDoc.LoadXml(navigator.OuterXml);
                outDoc.Save(stream);
            }
        }
    }
}
