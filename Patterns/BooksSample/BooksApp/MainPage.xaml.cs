using BooksApp.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace BooksApp
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            ViewModel = (Application.Current as App).AppServices.GetService<MainPageViewModel>();
            this.InitializeComponent();
            ViewModel.SetNavigationFrame(ContentFrame);
        }

        public MainPageViewModel ViewModel { get; }
    }
}
