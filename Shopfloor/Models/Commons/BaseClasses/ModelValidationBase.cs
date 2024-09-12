using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace Shopfloor.Models.Commons.BaseClasses
{
    internal class ModelValidationBase : INotifyDataErrorInfo
    {
        private readonly Dictionary<string, List<string>?> _propertyErrors = [];
        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
        public bool HasErrors => _propertyErrors.Count != 0;
        public void AddError(string propertyName, string errorMassage)
        {
            if (!_propertyErrors.TryGetValue(propertyName, out List<string>? errorsList))
            {
                errorsList = [];
                _propertyErrors.Add(propertyName, errorsList);
            }
            if (errorsList!.Count == 0)
            {
                errorsList.Add(errorMassage);
            }

            OnErrorsChanged(propertyName);
        }
        public void ClearErrors(string propertyName)
        {
            if (_propertyErrors.Remove(propertyName))
            {
                OnErrorsChanged(propertyName);
            }
        }
        public int CountErrors(string propertyName)
        {
            if (!_propertyErrors.TryGetValue(propertyName, out _))
            {
                return 0;
            }

            return _propertyErrors[propertyName]?.Count ?? 0;
        }
        public IEnumerable GetErrors(string? propertyName) => _propertyErrors.GetValueOrDefault(propertyName ?? string.Empty, null) ?? [];
        private void OnErrorsChanged(string propertyName) => ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
    }
}