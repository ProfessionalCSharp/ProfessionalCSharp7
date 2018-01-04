using DataBindingSamples.Models;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace DataBindingSamples.Views
{
    public sealed partial class BookUserControl : UserControl
    {
        public BookUserControl()
        {
            this.InitializeComponent();
            this.DataContext = this;
        }

        public Book Book
        {
            get { return (Book)GetValue(BookProperty); }
            set { SetValue(BookProperty, value); }
        }

        public static readonly DependencyProperty BookProperty =
            DependencyProperty.Register("Book", typeof(Book), typeof(BookUserControl), new PropertyMetadata(null));

    }
}
