using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using System.Threading.Tasks;

namespace WebHooksReceiver.Services
{
    public class StorageQueueService : IStorageQueueService
    {
        private readonly IConfiguration _configuration;
        public StorageQueueService(IConfiguration configuration) =>
            _configuration = configuration;

        private CloudStorageAccount _storageAccount;
        public CloudStorageAccount StorageAccount =>
            _storageAccount ?? (_storageAccount =
                CloudStorageAccount.Parse(_configuration.GetConnectionString("AzureStorageConnectionString")));

        private CloudQueueClient _queueClient;
        public CloudQueueClient QueueClient =>
            _queueClient ?? (_queueClient =
                StorageAccount.CreateCloudQueueClient());
        
        public async Task WriteToQueueStorageAsync(string queueName, string message)
        {
            CloudQueue queue = QueueClient.GetQueueReference(queueName);
            await queue.CreateIfNotExistsAsync();
            await queue.AddMessageAsync(new CloudQueueMessage(message));
        }
    }
}
