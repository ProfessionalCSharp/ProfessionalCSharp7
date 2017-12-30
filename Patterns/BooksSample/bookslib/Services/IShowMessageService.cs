using System.Threading.Tasks;

namespace BooksLib.Services
{
    public interface IShowMessageService
    {
        Task ShowMessageAsync(string message);
    }
}
