using Shopfloor.Interfaces;
using Shopfloor.Models;

namespace Shopfloor.Validators
{
    public class MachineValidation
    {
        private readonly IInputForm<Machine> _inputForm;
        public MachineValidation(IInputForm<Machine> inputForm)
        {
            _inputForm = inputForm;
        }
        #region NAME
        public void ValidateName(string value, string propertyName)
        {
            _inputForm.ClearErrors(propertyName);
            Name_CheckNull(value, propertyName);
            Name_CheckEmpty(value, propertyName);
        }
        private void Name_CheckEmpty(string value, string propertyName)
        {
            if (value.Trim().Length == 0) _inputForm.AddError(propertyName, "Nazwa maszyny nie może być pusta");
        }
        private void Name_CheckNull(string value, string propertyName)
        {
            if (value == null) _inputForm.AddError(propertyName, "Wprowadź nazwę maszyny");
        }
        #endregion NAME
    }
}