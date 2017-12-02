using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BooksServiceSample.Models
{
    public class BookChapter
    {
        public Guid Id { get; set; }
        [Required]
        public int Number { get; set; }
        [Required]
        [MaxLength(40)]
        public string Title { get; set; }
        [DefaultValue(0)]
        public int Pages { get; set; }
    }
}
