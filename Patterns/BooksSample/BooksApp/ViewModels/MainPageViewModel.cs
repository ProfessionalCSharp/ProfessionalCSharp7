using BooksApp.Services;
using BooksApp.Views;
using BooksLib.Services;
using Framework.Services;
using Framework.ViewModels;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;

namespace BooksApp.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private Dictionary<string, Type> _pages = new Dictionary<string, Type>
        {
            [PageNames.BooksPage] = typeof(BooksPage),
            [PageNames.BookDetailPage] = typeof(BookDetailPage)
        };

        private readonly INavigationService _navigationService;
        private readonly UWPInitializeNavigationService _initializeNavigationService;
        public MainPageViewModel(INavigationService navigationService, UWPInitializeNavigationService initializeNavigationService)
        {
            _navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));
            _initializeNavigationService = initializeNavigationService ?? throw new ArgumentNullException(nameof(initializeNavigationService));
        }

        public void SetNavigationFrame(Frame frame) => _initializeNavigationService.Initialize(frame, _pages);

        public void UseNavigation(bool navigation)
        {
            _navigationService.UseNavigation = navigation;
        }

        public void OnNavigationSelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.SelectedItem is NavigationViewItem navigationItem)
            {
                switch (navigationItem.Tag)
                {
                    case "books":
                        _navigationService.NavigateToAsync(PageNames.BooksPage);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
