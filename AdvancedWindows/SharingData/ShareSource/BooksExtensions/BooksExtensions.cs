using ShareSource.Models;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace ShareSource.BooksExtensions
{
    public static class BooksExtensions
    {
        public static string ToHtml(this IEnumerable<Book> books) =>
            new XElement("table",
                new XElement("thead",
                    new XElement("tr",
                        new XElement("td", "Title"),
                        new XElement("td", "Publisher"))),
                books.Select(b =>
                    new XElement("tr",
                        new XElement("td", b.Title),
                        new XElement("td", b.Publisher)))).ToString();

    }
}
