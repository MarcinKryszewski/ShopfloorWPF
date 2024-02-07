using Shopfloor.Interfaces;
using System.Collections.Generic;
using System.ComponentModel;

namespace Shopfloor.Models.UserModel
{
    public class UserValidation
    {
        #region AUTOLOGIN HANDLE
        private readonly INotifyDataErrorInfo? _caller;
        public UserValidation(INotifyDataErrorInfo? caller)
        {
            _caller = caller;
        }
        #endregion AUTOLOGIN HANDLE
        #region AUTOLOGIN
        public void ValidateAutoLogin(User? user, Dictionary<string, List<string>?> propertyErrors)
        {
            AutoLogin_ExistsInDatabase(user, propertyErrors);
        }
        private void AutoLogin_ExistsInDatabase(User? user, Dictionary<string, List<string>?> propertyErrors)
        {
            if (_caller is null) return;
            if (user is null)
            {
                if (!propertyErrors.ContainsKey("LoginFailed"))
                {
                    propertyErrors.Add("LoginFailed", []);
                }
            }
        }
        #endregion AUTOLOGIN
        private readonly IInputForm<User>? _inputForm;
        public UserValidation(IInputForm<User>? inputForm)
        {
            _inputForm = inputForm;
        }
        #region NAME
        public void ValidateName(string value, string propertyName)
        {
            if (_inputForm is null) return;
            IInputForm<User> inputForm = _inputForm;
            inputForm.ClearErrors(propertyName);
            Name_CheckNull(inputForm, value, propertyName);
            Name_CheckEmpty(inputForm, value, propertyName);
        }
        private static void Name_CheckEmpty(IInputForm<User> inputForm, string value, string propertyName)
        {
            if (value.Trim().Length == 0) inputForm.AddError(propertyName, "Nazwa użytkownika nie może być pusta");
        }
        private static void Name_CheckNull(IInputForm<User> inputForm, string value, string propertyName)
        {
            if (value == null) inputForm.AddError(propertyName, "Wprowadź nazwę użytkownika");
        }
        #endregion NAME
        public void ValidateLogin(User? user, IInputForm<User> inputForm)
        {
            string propertyName = "LoginError";
            inputForm.ClearErrors(propertyName);
            Login_ExistsInDatabase(inputForm, user, propertyName);
        }
        private static void Login_ExistsInDatabase(IInputForm<User> inputForm, User? value, string propertyName)
        {
            if (value == null) inputForm.AddError(propertyName, "Nie znaleziono użytkownika o takim loginie");
        }
    }
}