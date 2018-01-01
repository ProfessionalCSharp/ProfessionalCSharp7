using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Framework
{
    public abstract class ErrorInfoObject : BindableBase, INotifyDataErrorInfo
    {
        #region INotifyDataErrorInfo
        private bool _hasErrors;
        public bool HasErrors
        {
            get => _hasErrors;
            set => Set(ref _hasErrors, value);
        }

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public IEnumerable GetErrors(string propertyName)
        {
            if (_errors.TryGetValue(propertyName, out List<string> errorsList))
            {
                return errorsList;
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region Set and Clear Errors

        private Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();

        public virtual void SetError(string errorMessage, [CallerMemberName] string propertyName = null)
        {
            if (_errors.TryGetValue(propertyName, out List<string> errorList))
            {
                errorList.Add(errorMessage);
            }
            else
            {
                errorList = new List<string> { errorMessage };
                _errors.Add(propertyName, errorList);
            }
            HasErrors = true;
        }

        public void ClearErrors([CallerMemberName] string propertyName = null)
        {
            if (HasErrors)
            {
                if (_errors.ContainsKey(propertyName))
                {
                    _errors.Remove(propertyName);
                    if (_errors.Count == 0)
                    {
                        HasErrors = false;
                    }
                    ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
                }
            }
        }

        public void ClearAllErrors()
        {
            if (HasErrors)
            {
                _errors.Clear();
                HasErrors = false;
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(string.Empty));
            }
        }
        #endregion

    }
}
