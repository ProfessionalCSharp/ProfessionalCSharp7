using BooksLib.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace BooksApp.Views
{
    public sealed partial class BookDetailUserControl : UserControl
    {
        public BookDetailUserControl()
        {
            this.InitializeComponent();
        }

        public BooksViewModel ViewModel
        {
            get { return (BooksViewModel)GetValue(ManageBooksViewModelProperty); }
            set { SetValue(ManageBooksViewModelProperty, value); }
        }

        public static readonly DependencyProperty ManageBooksViewModelProperty =
            DependencyProperty.Register("MyProperty", typeof(BooksViewModel), typeof(BookDetailUserControl), new PropertyMetadata(null));
    }
}
