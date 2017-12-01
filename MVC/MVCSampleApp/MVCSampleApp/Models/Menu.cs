using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCSampleApp.Models
{
    public partial class Menu
    {
        public int Id { get; set; }
        [Required, StringLength(50)]
        public string Text { get; set; }
        [Display(Name = "Price"), DisplayFormat(DataFormatString = "{0:c}")]
        public double Price { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [StringLength(10)]
        public string Category { get; set; }
    }
}
