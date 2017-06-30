using System;
using System.Threading.Tasks;

namespace QueueSample
{
    class Program
    {
        static async Task Main()
        {
            var dm = new DocumentManager();

            Task processDocuments = ProcessDocuments.StartAsync(dm);

            // Create documents and add them to the DocumentManager
            for (int i = 0; i < 1000; i++)
            {
                var doc = new Document($"Doc {i}", "content");
                dm.AddDocument(doc);
                Console.WriteLine($"Added document {doc.Title}");
                await Task.Delay(new Random().Next(20));
            }
            await processDocuments;

            Console.ReadLine();
        }
    }
}
