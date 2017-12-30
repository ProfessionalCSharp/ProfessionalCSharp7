using System.Threading;

namespace Framework.ViewModels
{
    public abstract class ViewModelBase : BindableObject
    {
        private int _inProgressCounter = 0;
        protected void SetInProgress(bool set = true)
        {
            if (set)
            {
                Interlocked.Increment(ref _inProgressCounter);
                OnPropertyChanged(nameof(InProgress));
            }
            else
            {
                Interlocked.Decrement(ref _inProgressCounter);
                OnPropertyChanged(nameof(InProgress));
            }
        }

        public bool InProgress => _inProgressCounter != 0;

        private bool _hasError;
        public bool HasError
        {
            get => _hasError;
            set => Set(ref _hasError, value);
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set => Set(ref _errorMessage, value);
        }
    }
}
