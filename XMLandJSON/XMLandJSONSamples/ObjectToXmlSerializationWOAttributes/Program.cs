using System;
using System.IO;
using System.Xml.Serialization;

namespace ObjectToXmlSerializationSample
{
    class Program
    {
        //private const string ProductFileName = "product.xml";
        //private const string NewBooksFileName = "newbooks.xml";
        private const string InventoryFileName = "inventory.xml";
        private const string SerializeOption = "-s";
        private const string DeserializeOption = "-d";
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
                case SerializeOption:
                    SerializeInventory();
                    break;
                case DeserializeOption:
                    DeserializeInventory();
                    break;
                default:
                    ShowUsage();
                    break;
            }
        }

        private static void ShowUsage()
        {
            Console.WriteLine("ObjectToXmlSerializationSample options");
            Console.WriteLine("\tOptions");
            Console.WriteLine($"\t{SerializeOption}\tSerialize");
            Console.WriteLine($"\t{DeserializeOption}\tDeserialize");
        }
        public static void SerializeInventory()
        {
            var product = new Product
            {
                ProductID = 100,
                ProductName = "Product Thing",
                SupplierID = 10
            };

            var book = new BookProduct
            {
                ProductID = 101,
                ProductName = "How To Use Your New Product Thing",
                SupplierID = 10,
                ISBN = "1234567890"
            };

            Product[] products = { product, book };
            var inventory = new Inventory
            {
                InventoryItems = products
            };
       
            using (FileStream stream = File.Create(InventoryFileName))
            {
                var serializer = new XmlSerializer(typeof(Inventory), GetInventoryXmlAttributes());
                serializer.Serialize(stream, inventory);
            }
        }

        public static XmlAttributeOverrides GetInventoryXmlAttributes()
        {
            var inventoryAttributes = new XmlAttributes();
            inventoryAttributes.XmlArrayItems.Add(new XmlArrayItemAttribute("Book", typeof(BookProduct)));
            inventoryAttributes.XmlArrayItems.Add(new XmlArrayItemAttribute("Product", typeof(Product)));

            var bookIsbnAttributes = new XmlAttributes();
            bookIsbnAttributes.XmlAttribute = new XmlAttributeAttribute("Isbn");

            var productDiscountAttributes = new XmlAttributes();
            productDiscountAttributes.XmlAttribute = new XmlAttributeAttribute("Discount");

            var overrides = new XmlAttributeOverrides();

            overrides.Add(typeof(Inventory), "InventoryItems", inventoryAttributes);

            overrides.Add(typeof(BookProduct), "ISBN", bookIsbnAttributes);
            overrides.Add(typeof(Product), "Discount", productDiscountAttributes);
            return overrides;
        }

        public static void DeserializeInventory()
        {
            using (FileStream stream = File.OpenRead(InventoryFileName))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Inventory), GetInventoryXmlAttributes());
                Inventory newInventory = serializer.Deserialize(stream) as Inventory;
                foreach (Product prod in newInventory.InventoryItems)
                {
                    Console.WriteLine(prod.ProductName);
                }
            }
        }
    }
}
