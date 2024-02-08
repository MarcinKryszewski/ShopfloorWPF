using Shopfloor.Interfaces;

namespace Shopfloor.Models.ErrandPartModel
{
    internal sealed class ErrandPartValidation
    {
        private readonly IInputForm<ErrandPart> _inputForm;
        public ErrandPartValidation(IInputForm<ErrandPart> inputForm)
        {
            _inputForm = inputForm;
        }
        public void ValidateAmount(string propertyName, double? value)
        {
            _inputForm.ClearErrors(propertyName);
            Amount_CheckNull(propertyName, value);
            Amount_CheckZero(propertyName, value);
            Amount_CheckNegative(propertyName, value);
        }
        public void ValidateObjectAmount(ErrandPart errandPart, double? value)
        {
            string amountName = nameof(errandPart.Amount);
            errandPart.ClearErrors(amountName);
            if (value == 0) errandPart.AddError(amountName, "Podaj ilość");
            if (value is null) errandPart.AddError(amountName, "Podaj ilość");
            if (value < 0) errandPart.AddError(amountName, "Ilość nie może być ujemna");
        }
        private void Amount_CheckZero(string propertyName, double? value)
        {
            if (value == 0) _inputForm.AddError(propertyName, "Podaj ilość");
        }
        private void Amount_CheckNull(string propertyName, double? value)
        {
            if (value is null) _inputForm.AddError(propertyName, "Podaj ilość");
        }
        private void Amount_CheckNegative(string propertyName, double? value)
        {
            if (value < 0) _inputForm.AddError(propertyName, "Ilość nie może być ujemna");
        }
    }
}