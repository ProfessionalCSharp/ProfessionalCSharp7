using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace WindowsAppChatClient.Services
{
    public class MessagingService : IMessagingService
    {
        public async Task ShowMessageAsync(string message) => await new MessageDialog(message).ShowAsync();
    }
}
