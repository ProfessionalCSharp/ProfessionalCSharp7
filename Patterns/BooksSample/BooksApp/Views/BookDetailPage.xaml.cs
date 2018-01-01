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
        }

        public BookDetailViewModel ViewModel { get; } = (Application.Current as App).AppServices.GetService<BookDetailViewModel>();
    }
}
