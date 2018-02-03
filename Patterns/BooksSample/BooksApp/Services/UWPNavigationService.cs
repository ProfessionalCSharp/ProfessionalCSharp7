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
        private readonly UWPInitializeNavigationService _initializeNavigation;

        public UWPNavigationService(UWPInitializeNavigationService initializeNavigation)
        {
            _initializeNavigation = initializeNavigation ?? throw new ArgumentNullException(nameof(initializeNavigation));
        }

        public bool UseNavigation { get; set; }

        private Dictionary<string, Type> _pages;
        private Dictionary<string, Type> Pages => _pages ?? (_pages = _initializeNavigation.Pages);

        private Frame _frame;
        private Frame Frame => _frame ?? (_frame = _initializeNavigation.Frame);

        private string _currentPage;
        public string CurrentPage => _currentPage;

        public Task GoBackAsync()
        {
            PageStackEntry stackEntry = Frame.BackStack.Last();
            Type backPageType = stackEntry.SourcePageType;
            KeyValuePair<string, Type> pageEntry = Pages.FirstOrDefault(pair => pair.Value == backPageType);
            _currentPage = pageEntry.Key;
            
            Frame.GoBack();
            return Task.CompletedTask;
        }

        public Task NavigateToAsync(string pageName)
        {
            _currentPage = pageName;
            Frame.Navigate(Pages[pageName]);
            return Task.CompletedTask;
        }
    }
}
