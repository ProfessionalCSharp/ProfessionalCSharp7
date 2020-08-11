using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DefaultInterfaceMembersSample
{
    public interface IBindableBase : INotifyPropertyChanged
    {
        protected abstract void OnPropertyChanged(string propertyName);

        public virtual bool SetProperty<T>(ref T item, T value, [CallerMemberName] string propertyName = default!)
        {
            if (EqualityComparer<T>.Default.Equals(item, value)) return false;

            item = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }

    public class Entity
    {
        public int Id { get; set; }
    }

    public class Book : Entity, IBindableBase
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        void IBindableBase.OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private string? _title;
        public string? Title
        {
            get => _title;
            set => (this as IBindableBase).SetProperty(ref _title, value);
        }
    }
}
