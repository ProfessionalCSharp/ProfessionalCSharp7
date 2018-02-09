using System.Collections.Generic;

namespace QueueSample
{
    public class DocumentManager
    {
        private readonly object _syncQueue = new object();

        private readonly Queue<Document> _documentQueue = new Queue<Document>();

        public void AddDocument(Document doc)
        {
            lock (_syncQueue)
            {
                _documentQueue.Enqueue(doc);
            }
        }

        public Document GetDocument()
        {
            Document doc = null;
            lock (_syncQueue)
            {
                doc = _documentQueue.Dequeue();
            }
            return doc;
        }

        public bool IsDocumentAvailable => _documentQueue.Count > 0;
    }
}