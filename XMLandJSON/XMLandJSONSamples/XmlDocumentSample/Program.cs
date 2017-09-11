using System;
using System.IO;
using System.Xml;

namespace XmlDocumentSample
{
    class Program
    {
        private const string BooksFileName = "books.xml";
        private const string NewBooksFileName = "newbooks.xml";
        private const string ReadOption = "-r";
        private const string NavigateOption = "-n";
        private const string WriteOption = "-w";

        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                ShowUsage();
                return;
            }

            switch (args[0])
            {
                case ReadOption:
                    ReadXml();
                    break;
                case NavigateOption:
                    NavigateXml();
                    break;
                case WriteOption:
                    CreateXml();
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
            Console.WriteLine($"\t{ReadOption}\tRead XML");
            Console.WriteLine($"\t{WriteOption}\tWrite XML");
        }

        public static void ReadXml()
        {
            using (FileStream stream = File.OpenRead(BooksFileName))
            {
                var doc = new XmlDocument();
                doc.Load(stream);

                //get only the nodes that we want.
                XmlNodeList titleNodes = doc.GetElementsByTagName("title");

                //iterate through the XmlNodeList
                foreach (XmlNode node in titleNodes)
                {
                    Console.WriteLine(node.OuterXml);
                }
            }
        }

        public static void NavigateXml()
        {
            using (FileStream stream = File.OpenRead(BooksFileName))
            {
                var doc = new XmlDocument();
                doc.Load(stream);

                //get only the nodes that we want.
                XmlNodeList authorNodes = doc.GetElementsByTagName("author");

                //iterate through the XmlNodeList
                foreach (XmlNode node in authorNodes)
                {
                    Console.WriteLine($"Outer XML: {node.OuterXml}");
                    Console.WriteLine($"Inner XML: {node.InnerXml}");
                    Console.WriteLine($"Next sibling outer XML: {node.NextSibling.OuterXml}");
                    Console.WriteLine($"Previous sibling outer XML: {node.PreviousSibling.OuterXml}");
                    Console.WriteLine($"First child outer Xml: {node.FirstChild.OuterXml}");
                    Console.WriteLine($"Parent name: {node.ParentNode.Name}");
                    Console.WriteLine();
                }
            }
        }

        public static void CreateXml()
        {
            var doc = new XmlDocument();

            using (FileStream stream = File.OpenRead("books.xml"))
            {
                doc.Load(stream);
            }


            //create a new 'book' element
            XmlElement newBook = doc.CreateElement("book");
            //set some attributes
            newBook.SetAttribute("genre", "Mystery");
            newBook.SetAttribute("publicationdate", "2001");
            newBook.SetAttribute("ISBN", "123456789");
            //create a new 'title' element
            XmlElement newTitle = doc.CreateElement("title");
            newTitle.InnerText = "Case of the Missing Cookie";
            newBook.AppendChild(newTitle);
            //create new author element
            XmlElement newAuthor = doc.CreateElement("author");
            newBook.AppendChild(newAuthor);
            //create new name element
            XmlElement newName = doc.CreateElement("name");
            newName.InnerText = "Cookie Monster";
            newAuthor.AppendChild(newName);
            //create new price element
            XmlElement newPrice = doc.CreateElement("price");
            newPrice.InnerText = "9.95";
            newBook.AppendChild(newPrice);

            //add to the current document
            doc.DocumentElement.AppendChild(newBook);

            var settings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = "\t",
                NewLineChars = Environment.NewLine
            };
            //write out the doc to disk
            using (StreamWriter streamWriter = File.CreateText(NewBooksFileName))
            using (XmlWriter writer = XmlWriter.Create(streamWriter, settings))
            {
                doc.WriteContentTo(writer);
            }

            XmlNodeList nodeLst = doc.GetElementsByTagName("title");
            foreach (XmlNode node in nodeLst)
            {
                Console.WriteLine(node.OuterXml);
            }
        }
    }
}

