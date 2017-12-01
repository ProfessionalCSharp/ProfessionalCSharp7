using System.Collections.Generic;
using System.Threading.Tasks;
using MenuPlanner.Models;

namespace MenuPlanner.Services
{
    public interface IMenuCardsService
    {
        Task AddMenuAsync(Menu menu);
        Task DeleteMenuAsync(int id);
        Task<Menu> GetMenuByIdAsync(int id);
        Task<IEnumerable<MenuCard>> GetMenuCardsAsync();
        Task<IEnumerable<Menu>> GetMenusAsync();
        Task UpdateMenuAsync(Menu menu);
    }
}