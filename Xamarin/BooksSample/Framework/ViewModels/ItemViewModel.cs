namespace Framework.ViewModels
{
    public abstract class ItemViewModel<T> : BindableObject, IItemViewModel<T>
    {
        public virtual T Item { get; set; }
    }
}
