using System.Collections.Generic;
using System.Threading.Tasks;
using BooksLib.Models;

namespace BooksLib.Repositories
{
    public interface IBooksRepository
    {
        Task<Book> AddAsync(Book item);
        Task<bool> DeleteAsync(int id);
        Task<Book> GetItemAsync(int id);
        Task<IEnumerable<Book>> GetItemsAsync();
        Task<Book> UpdateAsync(Book item);
    }
}