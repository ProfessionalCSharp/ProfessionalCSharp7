using System;
using System.Collections.Generic;

namespace LinkedListSample
{
    public class PriorityDocumentManager
    {
        private readonly LinkedList<Document> _documentList;

        // priorities 0.9
        private readonly List<LinkedListNode<Document>> _priorityNodes;

        public PriorityDocumentManager()
        {
            _documentList = new LinkedList<Document>();

            _priorityNodes = new List<LinkedListNode<Document>>(10);
            for (int i = 0; i < 10; i++)
            {
                _priorityNodes.Add(new LinkedListNode<Document>(null));
            }
        }

        public void AddDocument(Document d)
        {
            if (d is null) throw new ArgumentNullException(nameof(d));

            AddDocumentToPriorityNode(d, d.Priority);
        }

        private void AddDocumentToPriorityNode(Document doc, int priority)
        {
            if (priority > 9 || priority < 0)
                throw new ArgumentException("Priority must be between 0 and 9");

            if (_priorityNodes[priority].Value == null)
            {
                --priority;
                if (priority >= 0)
                {
                    // check for the next lower priority
                    AddDocumentToPriorityNode(doc, priority);
                }
                else // now no priority node exists with the same priority or lower
                     // add the new document to the end
                {
                    _documentList.AddLast(doc);
                    _priorityNodes[doc.Priority] = _documentList.Last;
                }
                return;
            }
            else // a priority node exists
            {
                LinkedListNode<Document> prioNode = _priorityNodes[priority];
                if (priority == doc.Priority)
                // priority node with the same priority exists
                {
                    _documentList.AddAfter(prioNode, doc);

                    // set the priority node to the last document with the same priority
                    _priorityNodes[doc.Priority] = prioNode.Next;
                }
                else // only priority node with a lower priority exists
                {
                    // get the first node of the lower priority
                    LinkedListNode<Document> firstPrioNode = prioNode;

                    while (firstPrioNode.Previous != null &&
                       firstPrioNode.Previous.Value.Priority == prioNode.Value.Priority)
                    {
                        firstPrioNode = prioNode.Previous;
                        prioNode = firstPrioNode;
                    }

                    _documentList.AddBefore(firstPrioNode, doc);

                    // set the priority node to the new value
                    _priorityNodes[doc.Priority] = firstPrioNode.Previous;
                }
            }
        }

        public void DisplayAllNodes()
        {
            foreach (Document doc in _documentList)
            {
                Console.WriteLine($"priority: {doc.Priority}, title {doc.Title}");
            }
        }

        // returns the document with the highest priority
        // (that's first in the linked list)
        public Document GetDocument()
        {
            Document doc = _documentList.First.Value;
            _documentList.RemoveFirst();
            return doc;
        }
    }
}