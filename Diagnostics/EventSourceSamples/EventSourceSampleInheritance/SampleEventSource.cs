using System.Diagnostics.Tracing;

namespace EventSourceSampleInheritance
{
    public class SampleEventSource : EventSource
    {
        private SampleEventSource()
            : base("Wrox-SampleEventSource2") { }

        public static SampleEventSource Log = new SampleEventSource();
        public void Startup() => WriteEvent(1);
        
        public void CallService(string url) => WriteEvent(2, url);
        
        public void CalledService(string url, int length) => WriteEvent(3, url, length);
        
        public void ServiceError(string message, int error) => WriteEvent(4, message, error);
    }

}
