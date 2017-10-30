using System;

namespace ObjectToXmlSerializationSample
{
    public class Product
    {
        public int Discount { get; set; }
        public int ProductID { get; set; }   
        public string ProductName { get; set; }
        public int SupplierID { get; set; }
        public int CategoryID { get; set; }
        public string QuantityPerUnit { get; set; }
        public Decimal UnitPrice { get; set; }
        public short UnitsInStock { get; set; }
        public short UnitsOnOrder { get; set; }
        public short ReorderLevel { get; set; }
        public bool Discontinued { get; set; }
        public override string ToString() => $"{ProductID} {ProductName} {UnitPrice:C}";
    }
}
