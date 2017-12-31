using System.Collections.ObjectModel;

namespace Framework.ViewModels
{
    public abstract class MasterDetailViewModel<TItemViewModel, TItem> : ViewModelBase
    {
        private readonly ObservableCollection<TItemViewModel> _items = new ObservableCollection<TItemViewModel>();

        public ObservableCollection<TItemViewModel> Items => _items;

        protected TItemViewModel _selectedItem;
        public virtual TItemViewModel SelectedItem
        {
            get => _selectedItem;
            set
            {
                if (value == null) return;
                Set(ref _selectedItem, value);
            }
        }
    }
}
