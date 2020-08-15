using System.ComponentModel;

namespace DefaultInterfaceMembersSample
{
    public class Book : Entity, IBindableBase
    {
        public event PropertyChangedEventHandler PropertyChanged = (s, e) => { };

        void IBindableBase.OnPropertyChanged(string propertyName)
            => PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private string? _title;
        public string? Title
        {
            get => _title;
            set => (this as IBindableBase).SetProperty(ref _title, value);
        }
    }
}
