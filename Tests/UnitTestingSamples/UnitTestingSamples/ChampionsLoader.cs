using System.Xml.Linq;

namespace UnitTestingSamples
{
    public class ChampionsLoader : IChampionsLoader
    {
        public XElement LoadChampions() => XElement.Load(F1Addresses.RacersUrl);
    }
}
