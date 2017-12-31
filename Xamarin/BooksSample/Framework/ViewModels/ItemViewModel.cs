namespace Framework.ViewModels
{
    public abstract class ItemViewModel<T> : BindableObject, IItemViewModel<T>
    {
        public ItemViewModel(T item)
        {
            _item = item;
        }
        private readonly T _item;
        public virtual T Item => _item;
    }
}
