using System.Threading.Tasks;

namespace Framework.Services
{
    public interface INavigationService
    {
        bool UseNavigation { get; set; }
        Task NavigateToAsync(string page);
        Task GoBackAsync();
        string CurrentPage { get; }
    }
}
