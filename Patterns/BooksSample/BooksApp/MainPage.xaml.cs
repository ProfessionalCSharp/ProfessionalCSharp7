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
            this.InitializeComponent();
            ViewModel = (Application.Current as App).AppServices.GetService<MainPageViewModel>();
            ViewModel.SetNavigationFrame(ContentFrame);
        }

        public MainPageViewModel ViewModel { get; }
    }
}
