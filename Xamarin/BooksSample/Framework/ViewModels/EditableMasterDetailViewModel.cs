using Framework.Services;
using System.ComponentModel;

namespace Framework.ViewModels
{
    public abstract class EditableMasterDetailViewModel<TItemViewModel, TItem> : MasterDetailViewModel<TItemViewModel, TItem>, IEditableObject
        where TItemViewModel : IItemViewModel<TItem>
        where TItem : class
    {
        #region Commands
        public EditableMasterDetailViewModel(ISelectedItemService<TItem> selectedItemService)
        {
            _selectedItemService = selectedItemService;

            SaveCommand = new RelayCommand(EndEdit, () => IsEditMode);
            CancelEditModeCommand = new RelayCommand(CancelEdit, () => IsEditMode);
            EditModeCommand = new RelayCommand(BeginEdit, () => IsReadMode);
            RefreshCommand = new RelayCommand(OnRefresh);
            AddCommand = new RelayCommand(OnAdd, () => IsReadMode);
        }

        public RelayCommand RefreshCommand { get; }
        public RelayCommand AddCommand { get; }
        public RelayCommand EditModeCommand { get; }
        public RelayCommand CancelEditModeCommand { get; }
        public RelayCommand SaveCommand { get; }
        #endregion

        #region Edit / Read Mode
        private bool _isEditMode;
        public bool IsReadMode => !IsEditMode;
        public bool IsEditMode
        {
            get => _isEditMode;
            protected set
            {
                if (Set(ref _isEditMode, value))
                {
                    OnPropertyChanged(nameof(IsReadMode));
                    CancelEditModeCommand.OnCanExecuteChanged();
                    SaveCommand.OnCanExecuteChanged();
                    EditModeCommand.OnCanExecuteChanged();
                }
            }
        }

        // override from MasterDetailViewModel to change edit mode
        public override TItemViewModel SelectedItem
        {
            get => base.SelectedItem;
            set
            {
                if (value == null) return;

                if (Set(ref _selectedItem, value))
                {
                    IsEditMode = false;
                    _selectedItemService.SelectedItem = _selectedItem.Item;
                    OnPropertyChanged(nameof(EditItem));
                }
            }
        }
        #endregion

        #region Copy Item for Edit Mode
        private TItem _editItem;
        public TItem EditItem
        {
            get => _editItem ??  GetSelectedItem();
            private set => Set(ref _editItem, value);
        }

        protected virtual TItem GetSelectedItem() => SelectedItem?.Item;
        #endregion

        #region IEditableObject
        public virtual void BeginEdit()
        {
            IsEditMode = true;
            TItem itemCopy = CreateCopyOfItem(SelectedItem.Item);
            if (itemCopy != null)
            {
                EditItem = itemCopy;
            }
        }

        protected virtual TItem CreateCopyOfItem(TItem item) => null;

        public virtual void CancelEdit()
        {
            IsEditMode = false;
            EditItem = null;
            OnRefresh();
        }

        public virtual void EndEdit()
        {
            IsEditMode = false;
            OnSave();
            EditItem = null;
        }
        #endregion

        #region Overrides Needed By Derived Class
        protected abstract void OnSave();
        protected abstract void OnAdd();
        protected abstract void OnRefresh();
        #endregion
    }
}
