using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ValidationSample
{
    public abstract class NotifyDataErrorInfoBase : BindableObject,
        INotifyDataErrorInfo
    {
        private Dictionary<string, List<string>> _errors =
            new Dictionary<string, List<string>>();

        public void SetError(string errorMessage,
            [CallerMemberName] string propertyName = null)
        {
            List<string> errorList;
            if (_errors.TryGetValue(propertyName, out errorList))
            {
                errorList.Add(errorMessage);
            }
            else
            {
                errorList = new List<string> { errorMessage };
                _errors.Add(propertyName, errorList);
            }
            HasErrors = true;
            OnErrorsChanged(propertyName);
        }
        public void ClearErrors([CallerMemberName] string propertyName = null)
        {
            if (hasErrors)
            {
                List<string> errorList;
                if (_errors.TryGetValue(propertyName, out errorList))
                {
                    _errors.Remove(propertyName);
                }
                if (_errors.Count == 0)
                {
                    HasErrors = false;
                }
                OnErrorsChanged(propertyName);
            }
        }
        public void ClearAllErrors()
        {
            if (HasErrors)
            {
                _errors.Clear();
                HasErrors = false;
                OnErrorsChanged(null);
            }
        }
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public IEnumerable GetErrors(string propertyName)
        {
            List<string> errorsForProperty;
            bool err = _errors.TryGetValue(propertyName, out errorsForProperty);
            if (!err) return null;
            return errorsForProperty;
        }
        private bool hasErrors = false;
        public bool HasErrors
        {
            get { return hasErrors; }
            protected set
            {
                if (SetProperty(ref hasErrors, value))
                {
                    OnErrorsChanged(propertyName: null);
                }
            }
        }
        protected void OnErrorsChanged([CallerMemberName] string propertyName = null)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }
    }
}
