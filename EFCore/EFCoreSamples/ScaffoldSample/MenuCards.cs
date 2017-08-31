using System;
using System.Collections.Generic;

namespace ScaffoldSample
{
    public partial class MenuCards
    {
        public MenuCards()
        {
            Menus = new HashSet<Menus>();
        }

        public int MenuCardId { get; set; }
        public string Title { get; set; }

        public ICollection<Menus> Menus { get; set; }
    }
}
