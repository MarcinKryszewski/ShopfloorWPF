using System;
using Shopfloor.Interfaces;
using Shopfloor.Models.ErrandTypeModel;
using Shopfloor.Models.MachineModel;

namespace Shopfloor.Models.ErrandModel
{
    internal sealed class ErrandValidation
    {
        private readonly Errand _item;

        public ErrandValidation(Errand item)
        {
            _item = item;
        }
        public void Validate()
        {
            ValidateType();
            ValidateDescription();
            ValidateMachine();
        }
        public void ValidateType()
        {
            string propertyName = nameof(Errand.Type);
            ErrandType? value = _item.Type;

            _item.ClearErrors(propertyName);
            Type_CheckNull(propertyName, value);
        }
        private void Type_CheckNull(string propertyName, ErrandType? type)
        {
            if (type is null) _item.AddError(propertyName, "Wybierz rodzaj zadania");
        }
        public void ValidateMachine()
        {
            string propertyName = nameof(Errand.Machine);
            Machine? value = _item.Machine;

            _item.ClearErrors(propertyName);
            Machine_CheckNull(propertyName, value);
        }

        private void Machine_CheckNull(string propertyName, Machine? machine)
        {
            if (machine is null) _item.AddError(propertyName, "Przypisz maszynę do zadania");
        }

        public void ValidateDescription()
        {
            string propertyName = nameof(Errand.Description);
            string value = _item.Description;

            _item.ClearErrors(propertyName);
            Description_CheckNull(propertyName, value);
            Description_CheckEmpty(propertyName, value);
            Description_CheckLength(propertyName, value);
        }

        private void Description_CheckLength(string propertyName, string value)
        {
            int minDescriptionLength = 5;
            string lengthErrorMessage = "Opis jest za krótki. Minimum 5 znaków.";
            if (value?.Trim().Length < minDescriptionLength) _item.AddError(propertyName, lengthErrorMessage);
        }

        private void Description_CheckNull(string propertyName, string? value)
        {
            if (value?.Trim().Length == 0) _item.AddError(propertyName, "Opis nie może być pusty.");
        }

        private void Description_CheckEmpty(string propertyName, string? value)
        {
            if (value == null) _item.AddError(propertyName, "Wprowadź opis.");
        }
    }
}