using BooksApp.ViewModels;
using BooksLib.Events;
using Framework.Services;
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

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            EventAggregator<NavigationInfoEvent>.Instance.Publish(this, new NavigationInfoEvent { UseNavigation = e.NewSize.Width < 1024 });
        }
    }
}
