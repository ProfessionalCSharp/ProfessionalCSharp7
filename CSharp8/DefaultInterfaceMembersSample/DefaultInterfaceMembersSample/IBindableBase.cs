using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DefaultInterfaceMembersSample
{
    public interface IBindableBase : INotifyPropertyChanged
    {
        void OnPropertyChanged(string propertyName);

        public virtual bool SetProperty<T>(ref T item, T value, [CallerMemberName] string propertyName = default!)
        {
            if (EqualityComparer<T>.Default.Equals(item, value)) return false;

            item = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
