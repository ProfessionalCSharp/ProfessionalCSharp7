using System;
using System.Diagnostics.Tracing;

namespace ClientApp
{
    public class MyEventListener : EventListener
    {
        protected override void OnEventSourceCreated(EventSource eventSource)
        {
            Console.WriteLine($"created {eventSource.Name} {eventSource.Guid}");
        }

        protected override void OnEventWritten(EventWrittenEventArgs eventData)
        {
            Console.WriteLine($"event id: {eventData.EventId} source: {eventData.EventSource.Name}");
            foreach (var payload in eventData.Payload)
            {
                Console.WriteLine($"\t{payload}");
            }
        }
    }
}
