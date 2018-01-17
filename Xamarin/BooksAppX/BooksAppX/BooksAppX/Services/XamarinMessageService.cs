using BooksLib.Services;
using System.Threading.Tasks;

namespace BooksAppX.Services
{
    class XamarinMessageService : IMessageService
    {
        public Task ShowMessageAsync(string message)
        {
            // TODO: Implement Page Alert
            return Task.CompletedTask;
        }
    }
}
