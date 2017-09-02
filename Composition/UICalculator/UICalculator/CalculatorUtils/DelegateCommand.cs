using System;
using System.Windows.Input;

namespace Wrox.ProCSharp.Composition
{
    public class DelegateCommand : ICommand
    {
        private Action _execute;
        private Func<bool> _canExecute;

        public DelegateCommand(Action execute, Func<bool> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public DelegateCommand(Action execute)
            : this(execute, null) { }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => _canExecute?.Invoke() ?? true;

        public void Execute(object parameter) => _execute();


        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        
    }
}
