using BooksAppX.Views;
using BooksLib.Services;
using Framework.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BooksAppX.Services
{
    public class XamarinNavigationService : INavigationService
    {
        private Dictionary<string, Func<Page>> _pages = new Dictionary<string, Func<Page>>
        {
            [NavigationPageNames.BookDetailPage] = () => new BookDetailPage()
        };

        private INavigation _navigation;
        private INavigation Navigation
        {
            // get => _navigation ?? (_navigation = (Application.Current as App).MainPage.Navigation);
            get => _navigation ?? (_navigation = _initializeNavigation.Navigation);
        }

        private readonly InitializeNavigationService _initializeNavigation;

        public XamarinNavigationService(InitializeNavigationService initializeNavigation)
        {
            _initializeNavigation = initializeNavigation;
            //_navigation = (Application.Current as App).MainPage.Navigation;
            // _navigationPage = (Application.Current as App).MainPage as NavigationPage;
        }

        public Task GoBackAsync()
        {
            return Navigation.PopAsync();
        }

        public Task NavigateToAsync(string pagename)
        {
            return Navigation.PushAsync(_pages[pagename]());
        }
    }
}
