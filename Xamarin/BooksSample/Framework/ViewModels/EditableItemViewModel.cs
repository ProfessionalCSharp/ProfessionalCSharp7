using Framework.Services;
using System.ComponentModel;

namespace Framework.ViewModels
{
    public abstract class EditableItemViewModel<TItem> : ItemViewModel<TItem>, IEditableObject
        where TItem : class
    {
        private readonly IItemsService<TItem> _itemsService;

        public EditableItemViewModel(IItemsService<TItem> itemsService)
        {
            _itemsService = itemsService;
            Item = _itemsService.SelectedItem;

            SaveCommand = new RelayCommand(EndEdit, () => IsEditMode);
            CancelCommand = new RelayCommand(CancelEdit, () => IsEditMode);
            EditCommand = new RelayCommand(BeginEdit, () => IsReadMode);
            AddCommand = new RelayCommand(OnAdd, () => IsReadMode);
        }

        public RelayCommand AddCommand { get; }
        public RelayCommand EditCommand { get; }
        public RelayCommand CancelCommand { get; }
        public RelayCommand SaveCommand { get; }

        #region Edit / Read Mode
        private bool _isEditMode;
        public bool IsReadMode => !IsEditMode;
        public bool IsEditMode
        {
            get => _isEditMode;
            set
            {
                if (Set(ref _isEditMode, value))
                {
                    OnPropertyChanged(nameof(IsReadMode));
                    CancelCommand.OnCanExecuteChanged();
                    SaveCommand.OnCanExecuteChanged();
                    EditCommand.OnCanExecuteChanged();
                }
            }
        }

        #endregion

        #region Copy Item for Edit Mode
        private TItem _editItem;
        public TItem EditItem
        {
            get => _editItem ?? Item;
            set => Set(ref _editItem, value);
        }

        #endregion

        public abstract TItem CreateCopy(TItem item);

        #region Overrides Needed By Derived Class
        public abstract void OnSave();
        protected abstract void OnAdd();

        #endregion

        #region IEditableObject

        public virtual void BeginEdit()
        {
            IsEditMode = true;
            TItem itemCopy = CreateCopy(Item);
            if (itemCopy != null)
            {
                EditItem = itemCopy;
            }
        }

        public virtual void CancelEdit()
        {
            IsEditMode = false;
            EditItem = default(TItem);
        }

        public virtual void EndEdit()
        {
            IsEditMode = false;
            OnSave();
            EditItem = default(TItem);
        }
        #endregion
    }
}
