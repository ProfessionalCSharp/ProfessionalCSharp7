using System;
using System.Threading.Tasks;
namespace QueueSample
{
    public class ProcessDocuments
    {
        public static Task StartAsync(DocumentManager dm) =>
            Task.Run(new ProcessDocuments(dm).Run);

        protected ProcessDocuments(DocumentManager dm) =>
            _documentManager = dm ?? throw new ArgumentNullException(nameof(dm));

        private DocumentManager _documentManager;

        protected async Task Run()
        {
            while (true)
            {
                if (_documentManager.IsDocumentAvailable)
                {
                    Document doc = _documentManager.GetDocument();
                    Console.WriteLine($"Processing document {doc.Title}");
                }
                await Task.Delay(new Random().Next(20));
            }
        }
    }
}