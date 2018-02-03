using BooksLib.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace BooksApp.Views
{
    public sealed partial class BookDetailPage : Page
    {
        public BookDetailPage() => InitializeComponent();

        public BookDetailViewModel ViewModel { get; } = ApplicationServices.Instance.ServiceProvider.GetService<BookDetailViewModel>();

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;

            base.OnNavigatedTo(e);
        }
    }
}
