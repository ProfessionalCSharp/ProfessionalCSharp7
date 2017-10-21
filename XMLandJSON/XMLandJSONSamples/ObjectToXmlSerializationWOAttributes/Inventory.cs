using System.Text;

namespace ObjectToXmlSerializationSample
{
    public class Inventory
    {
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
