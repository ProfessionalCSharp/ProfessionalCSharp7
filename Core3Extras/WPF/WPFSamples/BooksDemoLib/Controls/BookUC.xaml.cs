using BooksDemo.Models;
using System.Windows;
using System.Windows.Controls;

namespace BooksDemo.Controls
{
    /// <summary>
    /// Interaction logic for BookUC.xaml
    /// </summary>
    public partial class BookUC : UserControl
    {
        public BookUC()
        {
            InitializeComponent();

            //var binding = new Binding { Path = new PropertyPath("Value"), Source = slider1 };
            //BindingOperations.SetBinding(scale1, ScaleTransform.ScaleXProperty, binding);
            //BindingOperations.SetBinding(scale1, ScaleTransform.ScaleYProperty, binding);
        }

        private void OnShowBook(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is Book theBook)
            {
                MessageBox.Show(theBook.Title, theBook.Isbn);
            }
        }

        private void OnChangeBook(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is Book theBook)
            {
                theBook.Title = "Professional C# 5";
                theBook.Isbn = "978-0-470-31442-5";
            }         
        }
    }
}
