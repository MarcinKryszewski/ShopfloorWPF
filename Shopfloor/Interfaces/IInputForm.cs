using System.ComponentModel;

namespace Shopfloor.Interfaces
{
    public interface IInputForm<T> : INotifyDataErrorInfo
    {
        public void CleanForm();

        public void ReloadData();

        public void AddError(string propertyName, string errorMassage);

        public void ClearErrors(string propertyName);

        public bool IsDataValidate();
    }
}