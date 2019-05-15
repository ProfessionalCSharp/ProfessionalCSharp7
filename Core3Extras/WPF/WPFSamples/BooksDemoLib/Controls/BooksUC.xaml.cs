using BooksDemo.Models;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace BooksDemo.Controls
{
    /// <summary>
    /// Interaction logic for BooksUC.xaml
    /// </summary>
    public partial class BooksUC : UserControl
    {
        public BooksUC() => InitializeComponent();

        private void OnAddBook(object sender, RoutedEventArgs e)
        {
            if (this.FindResource("books") is ObjectDataProvider odp && odp is IList<Book> books)
            {
                books.Add(new Book("HTML and CSS: Design and Build Websites", "Wiley", "978-1118-00818-8", "John Ducket"));
            }
        }
    }
}
