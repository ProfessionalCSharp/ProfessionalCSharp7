using BooksLib.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace BooksApp.Views
{
    public sealed partial class BookDetailPage : Page
    {
        public BookDetailPage()
        {
            this.InitializeComponent();
            ViewModel.UseNavigation = true; // if the Page is used, enable navigation
        }

        public BookDetailViewModel ViewModel { get; } = ApplicationServices.Instance.ServiceProvider.GetService<BookDetailViewModel>();
    }
}
