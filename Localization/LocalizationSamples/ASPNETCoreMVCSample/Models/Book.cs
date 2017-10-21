using System.ComponentModel;

namespace ASPNETCoreMVCSample.Models
{
    public class Book
    {
        [DisplayName("Booktitle")]
        public string Booktitle { get; set; }
        [DisplayName("Publisher")]
        public string Publisher { get; set; }
    }
}
