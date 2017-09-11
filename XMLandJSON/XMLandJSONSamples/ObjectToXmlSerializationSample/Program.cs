using System;
using System.IO;
using System.Xml.Serialization;

namespace ObjectToXmlSerializationSample
{
    class Program
    {
        private const string ProductFileName = "product.xml";
        private const string InventoryFileName = "inventory.xml";
        private const string SerializeOption = "-s";
        private const string DeserializeOption = "-d";
        private const string SerializeTreeOption = "-st";
        private const string DeserializeTreeOption = "-dt";

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
                    SerializeProduct();
                    break;
                case DeserializeOption:
                    DeserializeProduct();
                    break;
                case SerializeTreeOption:
                    SerializeInventory();
                    break;
                case DeserializeTreeOption:
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
            Console.WriteLine($"\t{SerializeTreeOption}\tSerialize Tree");
            Console.WriteLine($"\t{DeserializeTreeOption}\tDeserialize Tree");
        }

        public static void SerializeProduct()
        {
            //new products object
            var product = new Product
            {
                ProductID = 200,
                CategoryID = 100,
                Discontinued = false,
                ProductName = "Serialize Objects",
                QuantityPerUnit = "6",
                ReorderLevel = 1,
                SupplierID = 1,
                UnitPrice = 1000,
                UnitsInStock = 10,
                UnitsOnOrder = 0
            };

            FileStream stream = File.OpenWrite(ProductFileName);
            using (TextWriter writer = new StreamWriter(stream))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Product));
                serializer.Serialize(writer, product);
            }           
        }

        public static void DeserializeProduct()
        {
            Product product;
            using (var stream = new FileStream(ProductFileName, FileMode.Open))
            {
                var serializer = new XmlSerializer(typeof(Product));

                product = serializer.Deserialize(stream) as Product;
            }
            Console.WriteLine(product);
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
                var serializer = new XmlSerializer(typeof(Inventory));
                serializer.Serialize(stream, inventory);
            }
        }

        public static void DeserializeInventory()
        {
            using (FileStream stream = File.OpenRead(InventoryFileName))
            {
                var serializer = new XmlSerializer(typeof(Inventory));
                Inventory newInventory = serializer.Deserialize(stream) as Inventory;
                foreach (Product prod in newInventory.InventoryItems)
                {
                    Console.WriteLine(prod.ProductName);
                }
            }
        }
    }
}
