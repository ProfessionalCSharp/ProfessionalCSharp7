namespace Framework.ViewModels
{
    public abstract class ItemViewModel<T> : ViewModelBase, IItemViewModel<T>
    {
        public virtual T Item { get; set; }
    }
}
