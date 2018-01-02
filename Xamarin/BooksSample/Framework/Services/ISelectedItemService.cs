using System;

namespace Framework.Services
{
    public class SelectedItemEventArgs<T> : EventArgs
    {
        public SelectedItemEventArgs(T item)
        {
            Item = item;
        }
        T Item { get; set; }
    }

    public interface ISelectedItemService<T>
    {
        event EventHandler<SelectedItemEventArgs<T>> SelectedItemChanged;
        T SelectedItem { get; set; }
    }

    public class SelectedItemService<T> : ISelectedItemService<T>
    {
        private T _selectedItem;
        public T SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                SelectedItemChanged?.Invoke(this, new SelectedItemEventArgs<T>(SelectedItem));
            }
        }

        public event EventHandler<SelectedItemEventArgs<T>> SelectedItemChanged;
    }
}
