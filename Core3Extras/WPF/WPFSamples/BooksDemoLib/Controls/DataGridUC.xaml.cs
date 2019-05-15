using BooksDemo.Models;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace BooksDemo.Controls
{
    /// <summary>
    /// Interaction logic for DataGridUC.xaml
    /// </summary>
    public partial class DataGridUC : UserControl
    {
        private ListCollectionView _view;
        public DataGridUC()
        {
            _view = new ListCollectionView(new BooksRepository().GetBooks() as System.Collections.IList);
            InitializeComponent();
            // this.grid1.DataContext = view;            
        }
        public IEnumerable BooksView => _view;


        private void OnGroupChecked(object sender, RoutedEventArgs e)
        {
            if (_view.CanGroup)
            {
                if (_view.GroupDescriptions == null || _view.GroupDescriptions.Count == 0)
                {
                    // view.GroupDescriptions = new System.Collections.ObjectModel.ObservableCollection<System.ComponentModel.GroupDescription>();
                    _view.GroupDescriptions.Add(new PropertyGroupDescription("Publisher"));
                }
                else
                {
                    _view.GroupDescriptions.Clear();
                }
            }

        }
    }
}
