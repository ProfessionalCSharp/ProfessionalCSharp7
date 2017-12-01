using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace MVCSampleApp.Models
{
    [ModelMetadataType(typeof(MenuMetadata))]
    public partial class Menu
    {

    }
    public class MenuMetadata
    {
        public int Id { get; set; }
        [Required, StringLength(25)]
        public string Text { get; set; }
        [Display(Name = "Price"), DisplayFormat(DataFormatString = "{0:C}")]
        public double Price { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [StringLength(10)]
        public string Category { get; set; }
    }

}
