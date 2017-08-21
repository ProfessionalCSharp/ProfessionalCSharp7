using DISampleLib;
using System.Threading.Tasks;

namespace XamarinClient
{
    public class XamarinMessageService : IMessageService
    {
        private readonly IPageService _pageService;
        public XamarinMessageService(IPageService pageService)
        {
            _pageService = pageService;
        }

        public Task ShowMessageAsync(string message)
        {
            return _pageService.Page.DisplayAlert("Message", message, "Close");
        }
    }
}
