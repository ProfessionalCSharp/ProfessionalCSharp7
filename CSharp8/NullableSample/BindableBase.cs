using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NullableSampleApp
{
#nullable enable
    public abstract class BindableBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        // https://github.com/dotnet/corefx/blob/d58a51f911efb3c98beca21b6cf08cc703424fdf/src/Common/src/CoreLib/System/Collections/Generic/EqualityComparer.cs
        public bool SetProperty<T>(ref T item, T value,
            [CallerMemberName] string propertyName = default!)
        {
            if (EqualityComparer<T>.Default.Equals(item, value)) return false;

            item = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
#nullable restore
