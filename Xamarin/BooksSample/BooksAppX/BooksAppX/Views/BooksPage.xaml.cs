using BooksLib.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BooksAppX.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BooksPage : ContentPage
    {
        public BooksPage()
        {
            InitializeComponent();
            this.BindingContext = this;
        }

        public ManageBooksViewModel ViewModel { get; } = (Application.Current as App).AppServices.GetService<ManageBooksViewModel>();
    }
}