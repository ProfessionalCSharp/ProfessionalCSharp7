using BooksApp.Views;
using BooksLib.Services;
using Framework.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

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

        private string _currentPage;
        public string CurrentPage
        {
            get => _currentPage;
            set => _currentPage = value;
        }

        public Task GoBackAsync()
        {
            PageStackEntry stackEntry = Frame.BackStack.Last();
            Type backPageType = stackEntry.SourcePageType;
            KeyValuePair<string, Type> pageEntry = _pages.Where(pair => pair.Value == backPageType).FirstOrDefault();
            _currentPage = pageEntry.Key;
            
            Frame.GoBack();
            return Task.CompletedTask;
        }

        public Task NavigateToAsync(string pageName)
        {
            _currentPage = pageName;
            Frame.Navigate(_pages[pageName]);
            return Task.CompletedTask;
        }
    }
}
