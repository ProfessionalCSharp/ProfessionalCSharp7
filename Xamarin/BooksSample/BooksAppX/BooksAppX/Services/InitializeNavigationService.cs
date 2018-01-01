using System;
using Xamarin.Forms;

namespace BooksAppX.Services
{
    public class InitializeNavigationService
    {
        public void SetNavigation(INavigation navigation)
        {
            _navigation = navigation;
        }

        private INavigation _navigation;
        public INavigation Navigation => _navigation ?? throw new ArgumentException("navigation not initialized");
    }
}
