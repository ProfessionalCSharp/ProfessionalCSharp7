using System.ComponentModel.DataAnnotations;

namespace BlazorWasmSample.Shared
{
    public class Book
    {
        public int BookId { get; set; }
        [MaxLength(50)]
        public string Title { get; set; }
        [MaxLength(30)]
        public string Publisher { get; set; }
    }
}
