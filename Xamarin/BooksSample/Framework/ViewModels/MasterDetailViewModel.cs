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
        private readonly IEditModeService _editModeService;

        public MasterDetailViewModel(IItemsService<TItem> itemsService, IEditModeService editModeService)
        {
            _itemsService = itemsService;
            _editModeService = editModeService;
            _editModeService.EditModeChanged += (sender, mode) => AddCommand.OnCanExecuteChanged();

            RefreshCommand = new RelayCommand(OnRefresh);
            AddCommand = new RelayCommand(OnAdd, () => _editModeService.IsReadMode);
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
            set => _itemsService.SelectedItem = value;
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
            _editModeService.IsEditMode = false;
        }

        public virtual void OnAdd()
        {
            _editModeService.IsEditMode = true;
        }
    }
}
