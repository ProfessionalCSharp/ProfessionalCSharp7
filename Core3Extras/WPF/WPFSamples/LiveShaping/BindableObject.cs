using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LiveShaping
{
    public abstract class BindableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void SetProperty<T>(ref T item, T value, [CallerMemberName] string propertyName = null)
        {
            if (!EqualityComparer<T>.ReferenceEquals(item, value))
            {
                item = value;
                OnPropertyChanged(propertyName);
            }
        }
    }
}
