using BooksCacheClient.ViewModels;
using Windows.UI.Xaml.Controls;

namespace BooksCacheClient
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        public BooksViewModel ViewModel { get; } = new BooksViewModel();
    }
}
