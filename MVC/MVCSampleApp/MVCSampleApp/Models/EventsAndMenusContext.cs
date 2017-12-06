using System;
using System.Collections.Generic;

namespace MVCSampleApp.Models
{
    public class EventsAndMenusContext
    {
        private IEnumerable<Event> GetEvents() =>
            new List<Event>
            {
                new Event(1, "Formula 1 G.P. Australia, Melbourne", new DateTime(2018, 3, 25)),
                new Event(2, "Formula 1 G.P. Bahrain, Sakhir", new DateTime(2018, 4, 8)),
                new Event(3, "Formula 1 G.P. China, Shanghai", new DateTime(2018, 4, 15)),
                new Event(4, "Formula 1 G.P. Aserbaidschan, Baku", new DateTime(2018, 4, 29))
            };

        private IEnumerable<Menu> GetMenus() =>
            new List<Menu>
            {
                new Menu
                {
                    Id=1,
                    Text="Baby Back Barbecue Ribs",
                    Price=16.9,
                    Category="Main"
                },
                new Menu
                {
                    Id=2,
                    Text="Chicken and Brown Rice Piaf",
                    Price=12.9,
                    Category="Main"
                },
                new Menu
                {
                    Id=3,
                    Text="Chicken Miso Soup with Shiitake Mushrooms",
                    Price=6.9,
                    Category="Soup"
                }
            };

        private IEnumerable<Event> _events = null;
        public IEnumerable<Event> Events
        {
            get => _events ?? (_events = GetEvents());
        }
        private IEnumerable<Menu> _menus = null;
        public IEnumerable<Menu> Menus
        {
            get => _menus ?? (_menus = GetMenus());
        }
    }
}
