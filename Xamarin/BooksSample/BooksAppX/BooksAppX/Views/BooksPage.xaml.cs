using BooksLib.Models;
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

        public BooksViewModel ViewModel { get; } = (Application.Current as App).AppServices.GetService<BooksViewModel>();

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Book;
            if (item == null)
                return;

            ViewModel.SelectedItem = item;
            await Navigation.PushAsync(new BookDetailPage());

            // Manually deselect item.
           // ItemsListView.SelectedItem = null;
        }
    }
}