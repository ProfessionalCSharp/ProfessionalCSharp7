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
            ViewModel = ApplicationServices.Instance.ServiceProvider.GetService<MainPageViewModel>();
            ViewModel.SetNavigationFrame(ContentFrame);
        }

        public MainPageViewModel ViewModel { get; }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            ViewModel.UseNavigation(e.NewSize.Width < 1024);
            FireNavigation(e.NewSize.Width);
        }

        private void FireNavigation(double width) =>
            EventAggregator<NavigationInfoEvent>.Instance.Publish(this, new NavigationInfoEvent { UseNavigation = width < 1024 });
    }
}
