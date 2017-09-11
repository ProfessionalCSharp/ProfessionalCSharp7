using System.Xml.Serialization;

namespace ObjectToXmlSerializationSample
{
    public class BookProduct : Product
    {
        [XmlAttribute("Isbn")]
        public string ISBN { get; set; }
    }

}
