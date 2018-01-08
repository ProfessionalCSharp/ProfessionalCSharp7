using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Framework.Services
{
    public interface IItemsService<T>
    {
        Task RefreshAsync();

        Task<T> AddOrUpdateAsync(T item);

        Task DeleteAsync(T item);

        ObservableCollection<T> Items { get; }

        T SelectedItem { get; set; }
        event EventHandler<T> SelectedItemChanged;
    }
}
