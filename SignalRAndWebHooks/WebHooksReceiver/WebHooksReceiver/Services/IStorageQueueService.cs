using System.Threading.Tasks;

namespace WebHooksReceiver.Services
{
    public interface IStorageQueueService
    {
        Task WriteToQueueStorageAsync(string queueName, string message);
    }
}