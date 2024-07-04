using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Shopfloor.Shared.ViewModels
{
    internal sealed class ErrorsViewModel : ViewModelBase, INotifyDataErrorInfo
    {
        private readonly Dictionary<string, List<string>> _propertyErrors = new();

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
        public bool HasErrors => _propertyErrors.Any();
        public void AddError(string propertyName, string errorMessage)
        {
            if (!_propertyErrors.ContainsKey(propertyName))
            {
                _propertyErrors.Add(propertyName, new List<string>());
            }

            _propertyErrors[propertyName].Add(errorMessage);
            OnErrorsChanged(propertyName);
        }
        public void ClearErrors(string propertyName)
        {
            if (_propertyErrors.Remove(propertyName))
            {
                OnErrorsChanged(propertyName);
            }
        }
        public IEnumerable GetErrors(string? propertyName)
        {
            return _propertyErrors.GetValueOrDefault(propertyName, null);
        }
        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }
    }
}