namespace Framework.ViewModels
{
    public abstract class ItemViewModel<T> : ViewModelBase, IItemViewModel<T>
    {
        private T _item;
        public virtual T Item
        {
            get => _item;
            set => Set(ref _item, value);
        }
    }
}
