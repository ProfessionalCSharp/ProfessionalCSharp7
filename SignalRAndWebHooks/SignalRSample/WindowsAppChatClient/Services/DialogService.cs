using System;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace WindowsAppChatClient.Services
{
    public class DialogService : IDialogService
    {
        public async Task ShowMessageAsync(string message) => await new MessageDialog(message).ShowAsync();
    }
}
