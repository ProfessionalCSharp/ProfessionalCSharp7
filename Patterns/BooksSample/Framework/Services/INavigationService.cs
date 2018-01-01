using System.Threading.Tasks;

namespace Framework.Services
{
    public interface INavigationService
    {
        Task NavigateToAsync(string page);
        Task GoBackAsync();
    }
}
