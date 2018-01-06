using System;

namespace Framework.Services
{
    public class EventAggregator<TEvent>
        where TEvent : EventArgs
    {
        private static EventAggregator<TEvent> s_eventAggregator;
        public static EventAggregator<TEvent> Instance =>
            s_eventAggregator ?? (s_eventAggregator = new EventAggregator<TEvent>());

        private EventAggregator() { }

        public event Action<object, TEvent> Event;

        public void Publish(object source, TEvent ev) => Event?.Invoke(source, ev);
    }
}
