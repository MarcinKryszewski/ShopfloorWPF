using Shopfloor.Interfaces;
using Shopfloor.Models.ErrandTypeModel;
using Shopfloor.Models.MachineModel;

namespace Shopfloor.Models.ErrandModel
{
    internal sealed class ErrandValidation
    {
        private readonly IInputForm<Errand> _inputForm;

        public ErrandValidation(IInputForm<Errand> inputForm)
        {
            _inputForm = inputForm;
        }
        public void ValidateType(string propertyName, ErrandType? type)
        {
            _inputForm.ClearErrors(propertyName);
            Type_CheckNull(propertyName, type);
        }
        private void Type_CheckNull(string propertyName, ErrandType? type)
        {
            if (type is null) _inputForm.AddError(propertyName, "Wybierz rodzaj zadania");
        }
        public void ValidateMachine(string propertyName, Machine? machine)
        {
            _inputForm.ClearErrors(propertyName);
            Machine_CheckNull(propertyName, machine);
        }

        private void Machine_CheckNull(string propertyName, Machine? machine)
        {
            if (machine is null) _inputForm.AddError(propertyName, "Przypisz maszynę do zadania");
        }

        public void ValidateDescription(string propertyName, string? value)
        {
            _inputForm.ClearErrors(propertyName);
            Description_CheckNull(propertyName, value);
            Description_CheckEmpty(propertyName, value);
        }

        private void Description_CheckNull(string propertyName, string? value)
        {
            if (value?.Trim().Length == 0) _inputForm.AddError(propertyName, "Opis nie może być pusty");
        }

        private void Description_CheckEmpty(string propertyName, string? value)
        {
            if (value == null) _inputForm.AddError(propertyName, "Wprowadź opis");
        }
    }
}