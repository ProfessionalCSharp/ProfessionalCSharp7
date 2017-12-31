using System.ComponentModel;

namespace Framework.ViewModels
{
    public abstract class EditableItemViewModel<TItem> : ItemViewModel<TItem>, IEditableObject
        where TItem : class
    {
        public EditableItemViewModel(TItem item)
            : base(item)
        {
            SaveCommand = new RelayCommand(EndEdit, () => IsEditMode);
            CancelEditModeCommand = new RelayCommand(CancelEdit, () => IsEditMode);
            EditModeCommand = new RelayCommand(BeginEdit, () => IsReadMode);
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

        #endregion

        #region Copy Item for Edit Mode
        private TItem _editItem;
        public TItem EditItem
        {
            get => _editItem ?? Item;
            private set => Set(ref _editItem, value);
        }

        #endregion

        #region IEditableObject
        public virtual void BeginEdit()
        {
            IsEditMode = true;
            TItem itemCopy = CreateCopyOfItem(Item);
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
        #endregion
    }
}
