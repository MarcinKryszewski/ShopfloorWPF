using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Shopfloor.Shared.BaseClasses
{
    public class DataModel : INotifyDataErrorInfo
    {
        public bool HasErrors => _propertyErrors.Count != 0;
        private readonly Dictionary<string, List<string>?> _propertyErrors = [];
        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
        public IEnumerable GetErrors(string? propertyName) => _propertyErrors.GetValueOrDefault(propertyName ?? string.Empty, null) ?? [];
        private void OnErrorsChanged(string propertyName) => ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        public void AddError(string propertyName, string errorMassage)
        {
            if (!_propertyErrors.TryGetValue(propertyName, out List<string>? errorsList))
            {
                errorsList = [];
                _propertyErrors.Add(propertyName, errorsList);
            }
            if (errorsList!.Count == 0) errorsList?.Add(errorMassage);
            OnErrorsChanged(propertyName);
        }
        public void ClearErrors(string propertyName)
        {
            if (_propertyErrors.Remove(propertyName)) OnErrorsChanged(propertyName);
        }
    }
}