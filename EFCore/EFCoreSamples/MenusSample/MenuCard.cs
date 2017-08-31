using System;
using System.Collections.Generic;
using System.Text;

namespace MenusSample
{
    public class MenuCard
    {
        public int MenuCardId { get; set; }
        public string Title { get; set; }
        public List<Menu> Menus { get; } = new List<Menu>();
        public override string ToString() => Title;
    }

}
