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
        public BooksUC()
        {
            InitializeComponent();
        }

        private void OnAddBook(object sender, RoutedEventArgs e)
        {
            ((this.FindResource("books") as ObjectDataProvider).Data as IList<Book>).Add(new Book("HTML and CSS: Design and Build Websites", "Wiley", "978-1118-00818-8", "John Ducket"));
        }
    }
}
