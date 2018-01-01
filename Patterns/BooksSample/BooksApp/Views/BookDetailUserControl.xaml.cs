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

        public BookDetailViewModel ViewModel
        {
            get { return (BookDetailViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register("ViewModel", typeof(BookDetailViewModel), typeof(BookDetailUserControl), new PropertyMetadata(null));
    }
}
