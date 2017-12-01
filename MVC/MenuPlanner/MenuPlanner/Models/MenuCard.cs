using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MenuPlanner.Models
{
    public class MenuCard
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }
        public bool Active { get; set; }
        public int Order { get; set; }
        public virtual List<Menu> Menus { get; set; }
    }
}
