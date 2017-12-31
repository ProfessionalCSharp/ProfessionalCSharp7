using System;

namespace Framework.Services
{
    public class EditModeService : BindableObject, IEditModeService
    {
        public event EventHandler<bool> EditModeChanged;

        private bool _isEditMode;
        public bool IsEditMode
        {
            get => _isEditMode;
            set
            {
                if (Set(ref _isEditMode, value))
                {
                    OnPropertyChanged(nameof(IsReadMode));
                    EditModeChanged?.Invoke(this, IsEditMode);
                }
            }
        }
        public bool IsReadMode => !IsEditMode;
    }
}
