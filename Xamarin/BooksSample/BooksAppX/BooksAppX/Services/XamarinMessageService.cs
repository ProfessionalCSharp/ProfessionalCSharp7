using BooksLib.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BooksAppX.Services
{
    public class XamarinMessageService : IMessageService
    {
        public Task ShowMessageAsync(string message)
        {
            // TODO: Implement Page Alert
            return Task.CompletedTask;
        }
    }
}
