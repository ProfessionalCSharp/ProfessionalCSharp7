using System.Linq;
using System.Xml.Linq;

namespace UnitTestingSamples
{
    public class Formula1
    {
        private IChampionsLoader loader;
        public Formula1(IChampionsLoader loader)
        {
            this.loader = loader;
        }

        public XElement ChampionsByCountry(string country)
        {
            var q = from r in loader.LoadChampions().Elements("Racer")
                    where r.Element("Country").Value == country
                    orderby int.Parse(r.Element("Wins").Value) descending
                    select new XElement("Racer",
                      new XAttribute("Name", r.Element("Firstname").Value + " " +
                        r.Element("Lastname").Value),
                      new XAttribute("Country", r.Element("Country").Value),
                      new XAttribute("Wins", r.Element("Wins").Value));
            return new XElement("Racers", q.ToArray());
        }

        //public XElement ChampionsByCountry(string country)
        //{
        //    XElement champions = XElement.Load(F1Addresses.RacersUrl);

        //    var q = from r in champions.Elements("Racer")
        //            where r.Element("Country").Value == country
        //            orderby int.Parse(r.Element("Wins").Value) descending
        //            select new XElement("Racer",
        //              new XAttribute("Name", r.Element("Firstname").Value + " " +
        //                r.Element("Lastname").Value),
        //              new XAttribute("Country", r.Element("Country").Value),
        //              new XAttribute("Wins", r.Element("Wins").Value));
        //    return new XElement("Racers", q.ToArray());
        //}
    }
}
