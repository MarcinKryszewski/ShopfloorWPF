using Shopfloor.Interfaces;

namespace Shopfloor.Models.MachineModel
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
        #region PARENT
        public void ValidateParent(int? value, string propertyName, int? machineId)
        {
            _inputForm?.ClearErrors(propertyName);
            Parent_CheckTheSame(value, propertyName, machineId);
        }
        private void Parent_CheckTheSame(int? value, string propertyName, int? machineId)
        {
            if (value == null) return;
            if (machineId == null) return;
            if (value == machineId) _inputForm.AddError(propertyName, "Maszyna nie może być swoją maszyną nadrzędną");
        }
        #endregion
    }
}