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
            [PageNames.BookDetailPage] = () => new BookDetailPage()
        };

        private INavigation _navigation;
        private INavigation Navigation
        {
            get => _navigation ?? (_navigation = _initializeNavigation.Navigation);
        }

        public string CurrentPage => throw new NotImplementedException();

        private readonly XamarinInitializeNavigationService _initializeNavigation;

        public XamarinNavigationService(XamarinInitializeNavigationService initializeNavigation)
        {
            _initializeNavigation = initializeNavigation;
        }

        public Task GoBackAsync() => Navigation.PopAsync();

        public Task NavigateToAsync(string pagename) => Navigation.PushAsync(_pages[pagename]());
    }
}
