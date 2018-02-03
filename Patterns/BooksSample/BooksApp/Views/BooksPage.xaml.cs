using BooksLib.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace BooksApp.Views
{
    public sealed partial class BooksPage : Page
    {
        public BooksPage()
        {
            InitializeComponent();
            BookDetailUC.ViewModel = ApplicationServices.Instance.ServiceProvider.GetService<BookDetailViewModel>();
        }

        public BooksViewModel ViewModel { get; } = ApplicationServices.Instance.ServiceProvider.GetService<BooksViewModel>();

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;

            base.OnNavigatedTo(e);
        }
    }
}
