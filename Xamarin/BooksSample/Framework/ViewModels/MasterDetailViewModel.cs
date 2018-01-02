using Framework.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Framework.ViewModels
{
    public abstract class MasterDetailViewModel<TItemViewModel, TItem> : ViewModelBase
        where TItemViewModel : IItemViewModel<TItem>
    {
        private readonly IItemsService<TItem> _itemsService;

        public MasterDetailViewModel(IItemsService<TItem> itemsService)
        {
            _itemsService = itemsService;

            RefreshCommand = new RelayCommand(OnRefresh);
            AddCommand = new RelayCommand(OnAdd);
        }

        public RelayCommand RefreshCommand { get; }
        public RelayCommand AddCommand { get; }

        public ObservableCollection<TItem> Items => _itemsService.Items;

        protected abstract TItemViewModel ToViewModel(TItem item);

        public virtual IEnumerable<TItemViewModel> ItemsViewModels => _itemsService.Items.Select(item => ToViewModel(item));

        protected TItem _selectedItem;
        public virtual TItem SelectedItem
        {
            get => _itemsService.SelectedItem;
            set
            {
                _itemsService.SelectedItem = value;
                OnPropertyChanged();
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
