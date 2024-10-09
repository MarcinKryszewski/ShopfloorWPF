using System;
using System.Collections;
using System.ComponentModel;

namespace Shopfloor.Models.Commons.Interfaces
{
    internal interface IModelCreationModel<T>
    where T : IModel
    {
        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
        public bool HasErrors { get; }
        public void AddError(string propertyName, string errorMassage);
        public void ClearErrors(string propertyName);
        public int CountErrors(string propertyName);
        public IEnumerable GetErrors(string? propertyName);
        public T CreateModel(int id);
    }
}