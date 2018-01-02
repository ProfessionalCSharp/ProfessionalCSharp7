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
            get => _navigation ?? (_navigation = _initializeNavigation.Navigation);
        }

        private readonly InitializeNavigationService _initializeNavigation;

        public XamarinNavigationService(InitializeNavigationService initializeNavigation)
        {
            _initializeNavigation = initializeNavigation;
        }

        public Task GoBackAsync() => Navigation.PopAsync();

        public Task NavigateToAsync(string pagename) => Navigation.PushAsync(_pages[pagename]());
    }
}
