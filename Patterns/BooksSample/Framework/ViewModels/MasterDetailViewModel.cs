using Framework.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Framework.ViewModels
{
    public abstract class MasterDetailViewModel<TItemViewModel, TItem> : ViewModelBase
        where TItemViewModel : IItemViewModel<TItem>
        where TItem: class
    {
        private readonly IItemsService<TItem> _itemsService;

        public MasterDetailViewModel(IItemsService<TItem> itemsService)
        {
            _itemsService = itemsService;

            _itemsService.Items.CollectionChanged += (sender, e) =>
            {
                base.OnPropertyChanged(nameof(ItemsViewModels));
            };

            RefreshCommand = new RelayCommand(OnRefresh);
            AddCommand = new RelayCommand(OnAdd);
        }

        public RelayCommand RefreshCommand { get; }
        public RelayCommand AddCommand { get; }

        public ObservableCollection<TItem> Items => _itemsService.Items;

        protected abstract TItemViewModel ToViewModel(TItem item);

        public virtual IEnumerable<TItemViewModel> ItemsViewModels => Items.Select(item => ToViewModel(item));

        protected TItem _selectedItem;
        public virtual TItem SelectedItem
        {
            get => _itemsService.SelectedItem;
            set
            {
                if (!EqualityComparer<TItem>.Default.Equals(_itemsService.SelectedItem, value))
                {
                    _itemsService.SelectedItem = value;
                    OnPropertyChanged();
                }
            }
        }

        protected TItemViewModel _selectedItemViewModel;
        public virtual TItemViewModel SelectedItemViewModel
        {
            get => ToViewModel(_itemsService.SelectedItem);
            set
            {
                if (!EqualityComparer<TItem>.Default.Equals(SelectedItem, value?.Item)) 
                {
                    SelectedItem = value?.Item;
                    OnPropertyChanged();
                }
            }
        }

        public async void OnRefresh()
        {
            using (StartInProgress())
            {
                await OnRefreshAsync();
            }
        }

        protected async Task OnRefreshAsync()
        {
            await _itemsService.RefreshAsync();
        }

        public abstract void OnAdd();
    }
}
