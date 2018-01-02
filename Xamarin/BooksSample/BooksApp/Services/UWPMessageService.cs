using BooksLib.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace BooksApp.Services
{
    public class UWPMessageService : IMessageService
    {
        public async Task ShowMessageAsync(string message) => await new MessageDialog(message).ShowAsync();
    }
}
