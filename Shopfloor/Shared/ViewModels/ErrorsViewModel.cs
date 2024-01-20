using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Shopfloor.Shared.ViewModels
{
    public class ErrorsViewModel : ViewModelBase, INotifyDataErrorInfo
    {
        private readonly Dictionary<string, List<string>> _propertyErrors = new();

        public bool HasErrors => _propertyErrors.Any();

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        public IEnumerable GetErrors(string? propertyName)
        {
#pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
#pragma warning disable CS8603 // Possible null reference return.
            return _propertyErrors.GetValueOrDefault(propertyName, null);
#pragma warning restore CS8603 // Possible null reference return.
#pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
        }

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

        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }
    }
}