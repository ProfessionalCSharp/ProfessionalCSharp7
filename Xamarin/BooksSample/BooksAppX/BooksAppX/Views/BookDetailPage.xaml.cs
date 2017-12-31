using BooksLib.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BooksAppX.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BookDetailPage : ContentPage
    {
        public BookDetailPage()
        {
            InitializeComponent();
            this.BindingContext = this;
        }

        public BookDetailViewModel ViewModel { get; } = (Application.Current as App).AppServices.GetService<BookDetailViewModel>();
    }
}