using System.ComponentModel;

namespace Shopfloor.Interfaces
{
    internal interface IInputForm<T> : INotifyDataErrorInfo
    {
        public void CleanForm();

        public void AddError(string propertyName, string errorMassage);

        public void ClearErrors(string? propertyName);

        public bool IsDataValidate { get; }
    }
}