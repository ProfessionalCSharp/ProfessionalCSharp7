using System.ComponentModel;

namespace Framework
{
    public interface IEditableObjectContainer<TItem>
    {
        bool IsEditMode { get; set; }
        TItem CreateCopyOfItem(TItem item);
        TItem Item { get; }
        TItem EditItem { get; set; }
        void OnSave();
    }

    public class EditableObject<TItem> : IEditableObject
    {
        private readonly IEditableObjectContainer<TItem> _container;
        public EditableObject(IEditableObjectContainer<TItem> container)
        {
            _container = container;
        }

        public virtual void BeginEdit()
        {
            _container.IsEditMode = true;
            TItem itemCopy = _container.CreateCopyOfItem(_container.Item);
            if (itemCopy != null)
            {
                _container.EditItem = itemCopy;
            }
        }

        protected virtual TItem CreateCopyOfItem(TItem item) => default(TItem);

        public virtual void CancelEdit()
        {
            _container.IsEditMode = false;
            _container.EditItem = default(TItem);
        }

        public virtual void EndEdit()
        {
            _container.IsEditMode = false;
            _container.OnSave();
            _container.EditItem = default(TItem);
        }
    }
}
