using System.Diagnostics.Tracing;

namespace EventSourceSampleAnnotations
{
    [EventSource(Name = "EventSourceSample", Guid = "45FFF0E2-7198-4E4F-9FC3-DF6934680096")]
    public class SampleEventSource : EventSource
    {
        public class Keywords
        {
            public const EventKeywords Network = (EventKeywords)1;
            public const EventKeywords Database = (EventKeywords)2;
            public const EventKeywords Diagnostics = (EventKeywords)4;
            public const EventKeywords Performance = (EventKeywords)8;
        }

        public class Tasks
        {
            public const EventTask CreateMenus = (EventTask)1;
            public const EventTask QueryMenus = (EventTask)2;
        }

        private SampleEventSource()
        {
        }

        public static SampleEventSource Log = new SampleEventSource();

        [Event(1, Opcode = EventOpcode.Start, Level = EventLevel.Verbose)]
        public void Startup() => WriteEvent(1);

        [Event(2, Opcode = EventOpcode.Info, Keywords = Keywords.Network,
          Level = EventLevel.Verbose, Message = "{0}")]
        public void CallService(string url) => WriteEvent(2);

        [Event(3, Opcode = EventOpcode.Info, Keywords = Keywords.Network,
            Level = EventLevel.Verbose, Message = "{0} length: {1}")]
        public void CalledService(string url, int length) => WriteEvent(3, url, length);

        [Event(4, Opcode = EventOpcode.Info, Keywords = Keywords.Network,
          Level = EventLevel.Error, Message = "{0} error: {1}")]
        public void ServiceError(string message, int error) =>
            WriteEvent(4, message, error);

        [Event(5, Opcode = EventOpcode.Info, Task = Tasks.CreateMenus,
          Level = EventLevel.Verbose, Keywords = Keywords.Network)]
        public void SomeTask() => WriteEvent(5);
    }
}
