using BooksLib.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace BooksApp.Views
{
    public sealed partial class BooksPage : Page
    {
        public BooksPage()
        {
            InitializeComponent();
            ViewModel.UseNavigation = false;
            BookDetailUC.ViewModel = ApplicationServices.Instance.ServiceProvider.GetService<BookDetailViewModel>();
        }

        public BooksViewModel ViewModel { get; } = ApplicationServices.Instance.ServiceProvider.GetService<BooksViewModel>();


    }
}
