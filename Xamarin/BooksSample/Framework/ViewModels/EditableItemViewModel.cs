using Framework.Services;

namespace Framework.ViewModels
{
    public abstract class EditableItemViewModel<TItem> : ItemViewModel<TItem>, IEditableObjectContainer<TItem>
        where TItem : class
    {
        private readonly EditableObject<TItem> _editableObject;
        private readonly ISelectedItemService<TItem> _selectedItemService;

        public EditableItemViewModel(ISelectedItemService<TItem> selectedItemService)
        {
            _selectedItemService = selectedItemService;
            Item = _selectedItemService.SelectedItem;

            _editableObject = new EditableObject<TItem>(this);

            SaveCommand = new RelayCommand(_editableObject.EndEdit, () => IsEditMode);
            CancelEditModeCommand = new RelayCommand(_editableObject.CancelEdit, () => IsEditMode);
            EditModeCommand = new RelayCommand(_editableObject.BeginEdit, () => IsReadMode);
            AddCommand = new RelayCommand(OnAdd, () => IsReadMode);
        }

        public RelayCommand AddCommand { get; }
        public RelayCommand EditModeCommand { get; }
        public RelayCommand CancelEditModeCommand { get; }
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
                    CancelEditModeCommand.OnCanExecuteChanged();
                    SaveCommand.OnCanExecuteChanged();
                    EditModeCommand.OnCanExecuteChanged();
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

        public abstract TItem CreateCopyOfItem(TItem item);

        #region Overrides Needed By Derived Class
        public abstract void OnSave();
        protected abstract void OnAdd();
        #endregion
    }
}
