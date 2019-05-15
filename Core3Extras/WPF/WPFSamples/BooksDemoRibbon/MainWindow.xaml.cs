using BooksDemo.Controls;
using BooksDemo.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Ribbon;
using System.Windows.Input;

namespace BooksDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : RibbonWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private ObservableCollection<UIControlInfo> _userControls = new ObservableCollection<UIControlInfo>();
        public IEnumerable<UIControlInfo> Controls => _userControls;


        private void OnClose(object sender, ExecutedRoutedEventArgs e)
        {

            Application.Current.Shutdown();
        }


        private void OnShowBook(object sender, ExecutedRoutedEventArgs e)
        {
            var bookUI = new BookUC();
            bookUI.DataContext = new Book { Title = "Professional C# 5.0 and .NET 4.5.1", Publisher = "Wrox Press", Isbn = "978-0-470-50225-9" };
            this.tabControl1.SelectedIndex = this.tabControl1.Items.Add(new TabItem { Header = "Book", Content = bookUI });
        }

        private void OnShowBooksList(object sender, RoutedEventArgs e)
        {
            var booksUI = new BooksUC();
            this.tabControl1.SelectedIndex = this.tabControl1.Items.Add(new TabItem { Header = "Books", Content = booksUI });

        }

        private void OnShowGrid(object sender, RoutedEventArgs e)
        {
            //var gridUI = new DataGridUC();
            //this.tabControl1.SelectedIndex = this.tabControl1.Items.Add(new TabItem { Header = "DataGrid", Content = gridUI });
        }


        //private void OnShowBook(object sender, ExecutedRoutedEventArgs e)
        //{
        //    var bookUI = new BookUC();
        //    bookUI.DataContext = new Book { Title = "Professional C# 5.0 and .NET 4.5.1", Publisher = "Wrox Press", Isbn = "978-0-470-50225-9" };
        //    _userControls.Add(new UIControlInfo { Title = "Book", Content = bookUI });
        //}

        //private void OnShowBooksList(object sender, ExecutedRoutedEventArgs e)
        //{
        //    var booksUI = new BooksUC();
        //    _userControls.Add(new UIControlInfo { Title = "Books List", Content = booksUI });
        //}

        private void OnShowBooksGrid(object sender, ExecutedRoutedEventArgs e)
        {
            var booksGrid = new DataGridUC();
            this.tabControl1.SelectedIndex = this.tabControl1.Items.Add(new TabItem { Header = "Books Grid", Content = booksGrid });
//            _userControls.Add(new UIControlInfo { Title = "Books Grid", Content = booksGrid });
        }
    }
}
