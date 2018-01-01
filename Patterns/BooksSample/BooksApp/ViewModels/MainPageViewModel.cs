using BooksApp.Services;
using BooksLib.Services;
using Framework.Services;
using Framework.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace BooksApp.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly UWPInitializeNavigationService _initializeNavigationService;
        public MainPageViewModel(INavigationService navigationService, UWPInitializeNavigationService initializeNavigationService)
        {
            _navigationService = navigationService;
            _initializeNavigationService = initializeNavigationService;
        }

        public void SetNavigationFrame(Frame frame) => _initializeNavigationService.SetFrame(frame);

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
