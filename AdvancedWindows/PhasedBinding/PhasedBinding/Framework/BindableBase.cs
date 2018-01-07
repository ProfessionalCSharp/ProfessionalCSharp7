using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PhasedBinding.Framework
{
    public abstract class BindableBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void Set<T>(ref T item, T value, [CallerMemberName] string propertyName = null)
        {
            if (!EqualityComparer<T>.Default.Equals(item, value))
            {
                item = value;
                OnPropertyChanged(propertyName);
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
