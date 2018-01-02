using System;

namespace BooksLib.Events
{
    public class NavigationInfoEvent : EventArgs
    {
        public bool UseNavigation { get; set; }
    }
}
