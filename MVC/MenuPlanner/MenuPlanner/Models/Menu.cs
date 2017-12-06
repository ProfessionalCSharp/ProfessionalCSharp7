using System;

namespace MenuPlanner.Models
{
    public class Menu
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public decimal Price { get; set; }
        public bool Active { get; set; }
        public int Order { get; set; }
        public string Type { get; set; }
        public DateTime Day { get; set; }
        public int MenuCardId { get; set; }
        public virtual MenuCard MenuCard { get; set; }
    }
}
