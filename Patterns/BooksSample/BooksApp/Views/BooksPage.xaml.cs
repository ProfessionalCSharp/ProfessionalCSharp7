using BooksLib.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace BooksApp.Views
{
    public sealed partial class BooksPage : Page
    {
        public BooksPage()
        {
            this.InitializeComponent();
            ViewModel.UseNavigation = false;
            BookDetailUC.ViewModel = (Application.Current as App).AppServices.GetService<BookDetailViewModel>();
        }

        public BooksViewModel ViewModel { get; } = (Application.Current as App).AppServices.GetService<BooksViewModel>();
    }
}
