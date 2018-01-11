using System.Diagnostics.Tracing;

namespace MyApplicationEvents
{
    public class SampleEventSource : EventSource
    {
        private SampleEventSource()
          : base("Wrox-SampleEventSource") { }

        public static SampleEventSource Log = new SampleEventSource();

        public void ProcessingStart(int x) => WriteEvent(1, x);
        
        public void Processing(int x) => WriteEvent(2, x);
        
        public void ProcessingStop(int x) => WriteEvent(3, x);

        public void RequestStart() => WriteEvent(4);        

        public void RequestStop() => WriteEvent(5);
    }
}
