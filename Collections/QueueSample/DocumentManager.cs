using System.Collections.Generic;

namespace QueueSample
{
    public class DocumentManager
    {
        private readonly object syncQueue = new object();

        private readonly Queue<Document> _documentQueue = new Queue<Document>();

        public void AddDocument(Document doc)
        {
            lock (syncQueue)
            {
                _documentQueue.Enqueue(doc);
            }
        }

        public Document GetDocument()
        {
            Document doc = null;
            lock (syncQueue)
            {
                doc = _documentQueue.Dequeue();
            }
            return doc;
        }

        public bool IsDocumentAvailable => _documentQueue.Count > 0;
    }
}