using System.Threading.Tasks;

namespace WindowsAppChatClient.Services
{
    public interface IMessagingService
    {
        Task ShowMessageAsync(string message);
    }
}