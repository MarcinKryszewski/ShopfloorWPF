using System.Windows;

namespace Shopfloor.Interfaces
{
    public interface IInputForm<T>
    {
        public string ErrorMassage { get; set; }
        public Visibility HasErrorVisibility { get; }
        public void CleanForm();
        public bool IsDataValidate(T inputValue);
    }
}