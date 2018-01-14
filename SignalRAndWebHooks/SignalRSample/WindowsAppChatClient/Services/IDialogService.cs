using System.Threading.Tasks;

namespace WindowsAppChatClient.Services
{
    public interface IDialogService
    {
        Task ShowMessageAsync(string message);
    }
}