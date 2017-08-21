using DISampleLib;
using System;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace UWPClient
{
    public class UWPMessageService : IMessageService
    {
        public async Task ShowMessageAsync(string message) =>
            await new MessageDialog(message).ShowAsync();
    }
}
