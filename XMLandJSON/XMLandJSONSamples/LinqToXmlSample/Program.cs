using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Xml.Linq;

namespace LinqToXmlSample
{
    class Program
    {
        private const string HamletFileName = "hamlet.xml";
        private const string SaveFileName = "savehamlet.xml";
        private const string InventoryFileName = "inventory.xml";
        private const string LoadOption = "-l";
        private const string CreateOption = "-c";
        private const string WithNamespaceOption = "-n";
        private const string With2NamespaceOption = "-n2";
        private const string WithCommentsOption = "-co";
        private const string WithAttributesOption = "-a";
        private const string QueryOption = "-q";
        private const string FeedOption = "-f";
        private const string TransformingOption = "-t";
        private const string TransformingXmlOption = "-tx";

        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                ShowUsage();
                return;
            }

            switch (args[0])
            {
                case LoadOption:
                    LoadDocument();
                    break;
                case CreateOption:
                    CreateXml();
                    break;
                case WithNamespaceOption:
                    WithNamespace();
                    break;
                case With2NamespaceOption:
                    With2Namespace();
                    break;
                case WithCommentsOption:
                    WithComments();
                    break;
                case WithAttributesOption:
                    WithAttributes();
                    break;
                case QueryOption:
                    QueryHamlet();
                    break;
                case FeedOption:
                    QueryFeed();
                    break;
                case TransformingOption:
                    TransformingToObjects();
                    break;
                case TransformingXmlOption:
                    TransformingToXml();
                    break;
                default:
                    ShowUsage();
                    break;
            }

            Console.WriteLine("Press return to exit");
            Console.ReadLine();
        }

        private static void ShowUsage()
        {
            Console.WriteLine("LinqToXmlSample options");
            Console.WriteLine("\tOptions");
            Console.WriteLine($"\t{LoadOption}\tLoad XDocument");
            Console.WriteLine($"\t{CreateOption}\tCreate XML");
            Console.WriteLine($"\t{WithNamespaceOption}\tWith Namespace");
            Console.WriteLine($"\t{With2NamespaceOption}\tWith 2 Namespaces");
            Console.WriteLine($"\t{WithCommentsOption}\tWith comments");
            Console.WriteLine($"\t{WithAttributesOption}\tWith attributes");
            Console.WriteLine($"\t{QueryOption}\tLINQ query");
            Console.WriteLine($"\t{FeedOption}\tLINQ query ATOM feed");
            Console.WriteLine($"\t{TransformingOption}\tTransform to Objects");
            Console.WriteLine($"\t{TransformingOption}\txTransform to XML");
        }

        public static void LoadDocument()
        {
            XDocument doc = XDocument.Load(HamletFileName);
            Console.WriteLine($"root name: {doc.Root.Name}");
            Console.WriteLine($"has root attributes? {doc.Root.HasAttributes}");
        }

        public static void CreateXml()
        {
            var company = new XElement("Company",
                new XElement("CompanyName", "Microsoft Corporation"),
                new XElement("CompanyAddress",
                    new XElement("Address", "One Microsoft Way"),
                    new XElement("City", "Redmond"),
                    new XElement("Zip", "WA 98052-6399"),
                    new XElement("State", "WA"),
                    new XElement("Country", "USA")));

            Console.WriteLine(company);
        }

        public static void WithNamespace()
        {
            XNamespace ns = "http://www.cninnovation.com/samples/2018";

            var company = new XElement(ns + "Company",
                new XElement("CompanyName", "Microsoft Corporation"),
                new XElement("CompanyAddress",
                    new XElement("Address", "One Microsoft Way"),
                    new XElement("City", "Redmond"),
                    new XElement("Zip", "WA 98052-6399"),
                    new XElement("State", "WA"),
                    new XElement("Country", "USA")));

            Console.WriteLine(company);
        }

        public static void With2Namespace()
        {
            XNamespace ns1 = "http://www.cninnovation.com/samples/2018";
            XNamespace ns2 = "http://www.cninnovation.com/samples/2018/address";

            var company = new XElement(ns1 + "Company",
                new XElement(ns2 + "CompanyName", "Microsoft Corporation"),
                new XElement(ns2 + "CompanyAddress",
                    new XElement(ns2 + "Address", "One Microsoft Way"),
                    new XElement(ns2 + "City", "Redmond"),
                    new XElement(ns2 + "Zip", "WA 98052-6399"),
                    new XElement(ns2 + "State", "WA"),
                    new XElement(ns2 + "Country", "USA")));

            Console.WriteLine(company);
        }

        public static void WithComments()
        {
            var doc = new XDocument();

            XComment comment = new XComment("Sample XML for Professional C#.");
            doc.Add(comment);

            var company = new XElement("Company",
                new XElement("CompanyName", "Microsoft Corporation"),
                new XComment("A great company"),
                new XElement("CompanyAddress",
                    new XElement("Address", "One Microsoft Way"),
                    new XElement("City", "Redmond"),
                    new XElement("Zip", "WA 98052-6399"),
                    new XElement("State", "WA"),
                    new XElement("Country", "USA")));
            doc.Add(company);

            Console.WriteLine(doc);
        }

        public static void WithAttributes()
        {
            var company = new XElement("Company",
                new XElement("CompanyName", "Microsoft Corporation"),
                new XAttribute("TaxId", "91-1144442"),
                new XComment("A great company"),
                new XElement("CompanyAddress",
                    new XElement("Address", "One Microsoft Way"),
                    new XElement("City", "Redmond"),
                    new XElement("Zip", "WA 98052-6399"),
                    new XElement("State", "WA"),
                    new XElement("Country", "USA")));

            Console.WriteLine(company);
        }

        public static void QueryHamlet()
        {
            XDocument doc = XDocument.Load(HamletFileName);

            IEnumerable<string> personas = (from people in doc.Descendants("PERSONA")
                                            select people.Value).ToList();

            Console.WriteLine($"{personas.Count()} Players Found");
            Console.WriteLine();

            foreach (var item in personas)
            {
                Console.WriteLine(item);
            }
        }

        public static async void QueryFeed()
        {
            try
            {
                var httpClient = new HttpClient();
                using (Stream stream = await httpClient.GetStreamAsync("http://csharp.christiannagel.com/feed/atom/"))
                {

                    XNamespace ns = "http://www.w3.org/2005/Atom";
                    XDocument doc = XDocument.Load(stream);

                    Console.WriteLine($"Title: {doc.Root.Element(ns + "title").Value}");
                    Console.WriteLine($"Subtitle: {doc.Root.Element(ns + "subtitle").Value}");
                    string url = doc.Root.Elements(ns + "link").Where(e => e.Attribute("rel").Value == "alternate").FirstOrDefault()?.Attribute("href")?.Value;
                    Console.WriteLine($"Link: {url}");
                    Console.WriteLine();

                    var queryPosts = from myPosts in doc.Descendants(ns + "entry")
                                     select new
                                     {
                                         Title = myPosts.Element(ns + "title")?.Value,
                                         Published =
                                           DateTime.Parse(myPosts.Element(ns + "published")?.Value),
                                         Summary = myPosts.Element(ns + "summary")?.Value,
                                         Url = myPosts.Element(ns + "link")?.Value,
                                         Comments = myPosts.Element(ns + "comments")?.Value
                                     };

                    foreach (var item in queryPosts)
                    {
                        string shortTitle = item.Title.Length > 50 ? item.Title.Substring(0, 50) + "..." : item.Title;
                        Console.WriteLine(shortTitle);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void TransformingToObjects()
        {
            XDocument doc = XDocument.Load(HamletFileName);
            var groups = 
                doc.Descendants("PGROUP")
                    .Select((g, i) =>
                        new
                        {
                            Number = i + 1,
                            Description = g.Element("GRPDESCR").Value,
                            Characters = g.Elements("PERSONA").Select(p => p.Value)
                        });

            foreach (var group in groups)
            {
                Console.WriteLine(group.Number);
                Console.WriteLine(group.Description);
                foreach (var name in group.Characters)
                {
                    Console.WriteLine(name);
                }
                Console.WriteLine();
            }
        }

        public static void TransformingToXml()
        {
            XDocument doc = XDocument.Load(HamletFileName);
            var hamlet = 
                new XElement("hamlet",
                    doc.Descendants("PGROUP")
                        .Select((g, i) =>
                            new XElement("group",
                                new XAttribute("number", i + 1),
                                new XAttribute("description", g.Element("GRPDESCR").Value),
                                new XElement("characters",
                                    g.Elements("PERSONA").Select(p => new XElement("name", p.Value))
                                ))));

            Console.WriteLine(hamlet);
        }
    }
}
