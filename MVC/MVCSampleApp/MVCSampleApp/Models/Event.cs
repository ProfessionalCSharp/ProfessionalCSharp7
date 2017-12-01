using System;

namespace MVCSampleApp.Models
{
    public class Event
    {
        public Event(int id, string text, DateTime day)
        {
            Id = id;
            Text = text;
            Day = day;
        }
        public int Id { get; }
        public string Text { get; }
        public DateTime Day { get; }
    }
}
