using System;
using Xamarin.Forms;

namespace BooksAppX.Services
{
    public class XamarinInitializeNavigationService
    {
        public void SetNavigation(INavigation navigation, string initialPage)
        {
            _navigation = navigation;
            _initialPage = initialPage;
        }

        private string _initialPage;
        private INavigation _navigation;
        public INavigation Navigation => _navigation ?? throw new ArgumentException("navigation not initialized");

        public string IntialPage => _initialPage;
    }
}
