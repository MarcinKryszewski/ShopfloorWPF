using System.Text.RegularExpressions;

namespace Shopfloor.Models.UserModel
{
    internal sealed partial class UserValidation
    {
        public UserValidation(User user)
        {
            _user = user;
        }
        private readonly User _user;
        public void ValidateAll()
        {
            ValidateName();
            ValidateSurname();
            ValidateUsername();
        }
        public void ValidateName()
        {
            string propertyName = nameof(User.Name);
            _user.ClearErrors(propertyName);
            Name_CheckCharacters(propertyName, _user.Name);
            Name_CheckLength(propertyName, _user.Name);
        }
        public void ValidateSurname()
        {
            string propertyName = nameof(User.Surname);
            _user.ClearErrors(propertyName);
            Surname_CheckLength(propertyName, _user.Surname);
        }
        public void ValidateUsername()
        {
            string propertyName = nameof(User.Username);
            _user.ClearErrors(propertyName);
            Username_CheckLength(propertyName, _user.Username);
            Username_CheckCharacters(propertyName, _user.Username);
        }
        private void Name_CheckCharacters(string propertyName, string propertyValue)
        {
            string errorText = "Imię nie może zawierać spacji, liczb ani znaków specjalnych";
            if (PunctuationMathCurrencyWhitespaceDigit().IsMatch(propertyValue)) _user.AddError(propertyName, errorText);
        }
        private void Name_CheckLength(string propertyName, string propertyValue)
        {
            string errorText = "Imię za krótkie";
            if (propertyValue.Trim().Length < 3) _user.AddError(propertyName, errorText);
        }
        private void Surname_CheckLength(string propertyName, string propertyValue)
        {
            string errorText = "Nazwisko jest za krótkie";
            if (propertyValue.Trim().Length < 2) _user.AddError(propertyName, errorText);
        }
        private void Username_CheckCharacters(string propertyName, string propertyValue)
        {
            string errorText = "Login nie może zawierać polskich liter, znaków specjalnych i spacji";
            if (WhitespaceNonWord().IsMatch(propertyValue)) _user.AddError(propertyName, errorText);
        }
        private void Username_CheckLength(string propertyName, string propertyValue)
        {
            string errorText = "Login musi być dłuższy niż 5 znaków";
            if (propertyValue.Length < 6) _user.AddError(propertyName, errorText);
        }
        [GeneratedRegex(@"[\p{P}\p{S}\s\d]")]
        private static partial Regex PunctuationMathCurrencyWhitespaceDigit();
        [GeneratedRegex(@"[\s\W]")]
        private static partial Regex WhitespaceNonWord();
    }
}