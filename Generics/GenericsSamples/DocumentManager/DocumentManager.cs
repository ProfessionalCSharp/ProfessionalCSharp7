using System;
using System.Collections.Generic;

namespace Wrox.ProCSharp.Generics
{
    public class DocumentManager<TDocument>
        where TDocument : IDocument
    {
        private readonly Queue<TDocument> documentQueue = new Queue<TDocument>();
        private readonly object lockQueue = new object();

        public void AddDocument(TDocument doc)
        {
            lock (lockQueue)
            {
                documentQueue.Enqueue(doc);
            }
        }

        public bool IsDocumentAvailable => documentQueue.Count > 0;

        public void DisplayAllDocuments()
        {
            foreach (TDocument doc in documentQueue)
            {
                Console.WriteLine(doc.Title);
            }
        }

        public TDocument GetDocument()
        {
            TDocument doc = default(TDocument);
            lock (lockQueue)
            {
                doc = documentQueue.Dequeue();
            }
            return doc;
        }
    }
}