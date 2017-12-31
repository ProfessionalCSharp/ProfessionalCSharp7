﻿using System.ComponentModel;

namespace Framework.ViewModels
{
    public abstract class EditableMasterDetailViewModel<TItemViewModel, TItem> : MasterDetailViewModel<TItemViewModel, TItem>, IEditableObject
        where TItemViewModel : IItemViewModel<TItem>
        where TItem : class
    {
        public EditableMasterDetailViewModel()
        {
            SaveCommand = new RelayCommand(EndEdit, () => IsEditMode);
            CancelEditModeCommand = new RelayCommand(CancelEdit, () => IsEditMode);
            EditModeCommand = new RelayCommand(BeginEdit, () => IsReadMode);
        }

        public RelayCommand EditModeCommand { get; }
        public RelayCommand CancelEditModeCommand { get; }
        public RelayCommand SaveCommand { get; }

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

        private TItem _editItem;
        public TItem EditItem
        {
            get => _editItem ??  GetSelectedItem();
            private set => Set(ref _editItem, value);
        }

        protected virtual TItem GetSelectedItem() => SelectedItem?.Item;

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
        }

        public virtual void EndEdit()
        {
            IsEditMode = false;
            OnSave();
            EditItem = null;
        }

        protected virtual void OnSave() { }

        public override TItemViewModel SelectedItem
        {
            get => base.SelectedItem;
            set
            {
                if (value == null) return;

                if (Set(ref _selectedItem, value))
                {
                    IsEditMode = false;
                    OnPropertyChanged(nameof(EditItem));
                }
            }
        }
    }
}