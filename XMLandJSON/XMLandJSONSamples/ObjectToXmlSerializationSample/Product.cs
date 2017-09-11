using System;
using System.Xml;
using System.Xml.Serialization;

namespace ObjectToXmlSerializationSample
{   
    [XmlRoot]
    public class Product
    {

        [XmlAttribute(AttributeName = "Discount")]
        public int Discount { get; set; }

        [XmlElement]
        public int ProductID { get; set; }

        [XmlElement]
        public string ProductName { get; set; }

        [XmlElement]
        public int SupplierID { get; set; }

        [XmlElement]
        public int CategoryID { get; set; }

        [XmlElement]
        public string QuantityPerUnit { get; set; }

        [XmlElement]
        public Decimal UnitPrice { get; set; }

        [XmlElement]
        public short UnitsInStock { get; set; }

        [XmlElement]
        public short UnitsOnOrder { get; set; }

        [XmlElement]
        public short ReorderLevel { get; set; }

        [XmlElement]
        public bool Discontinued { get; set; }

        public override string ToString() => $"{ProductID} {ProductName} {UnitPrice:C}";
    }

}
