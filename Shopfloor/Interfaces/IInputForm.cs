using System.ComponentModel;

namespace Shopfloor.Interfaces
{
    internal interface IInputForm<T> : INotifyDataErrorInfo
    {
        public bool IsDataValidate { get; }
        public void AddError(string propertyName, string errorMassage);
        public void CleanForm();
        public void ClearErrors(string? propertyName);
    }
}