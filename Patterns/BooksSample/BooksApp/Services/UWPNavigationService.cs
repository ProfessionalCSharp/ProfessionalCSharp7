using BooksApp.Views;
using BooksLib.Services;
using Framework.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace BooksApp.Services
{
    public class UWPNavigationService : INavigationService
    {
        private Dictionary<string, Type> _pages = new Dictionary<string, Type>
        {
            [PageNames.BooksPage] = typeof(BooksPage),
            [PageNames.BookDetailPage] = typeof(BookDetailPage)
        };

        private readonly UWPInitializeNavigationService _initializeNavigation;
        public UWPNavigationService(UWPInitializeNavigationService initializeNavigation)
        {
            _initializeNavigation = initializeNavigation;
        }

        private Frame _frame;
        private Frame Frame => _frame ?? (_frame = _initializeNavigation.Frame);

        public Task GoBackAsync()
        {
            Frame.GoBack();
            return Task.CompletedTask;
        }

        public Task NavigateToAsync(string pagename)
        {
            Frame.Navigate(_pages[pagename]);
            return Task.CompletedTask;
        }
    }
}
