using System.Text;
using System.Xml.Serialization;

namespace ObjectToXmlSerializationSample
{
    [XmlRoot]
    public class Inventory
    {
        [XmlArrayItem("Product", typeof(Product)), XmlArrayItem("Book", typeof(BookProduct))]
        public Product[] InventoryItems { get; set; }

        public override string ToString()
        {
            var outText = new StringBuilder();
            foreach (Product prod in InventoryItems)
            {
                outText.AppendLine(prod.ProductName);
            }
            return outText.ToString();
        }
    }


}
