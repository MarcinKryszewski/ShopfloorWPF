using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using Shopfloor.Features.Login.Commands;
using Shopfloor.Features.Mechanic;
using Shopfloor.Interfaces;
using Shopfloor.Models.UserModel;
using Shopfloor.Services.NavigationServices;
using Shopfloor.Shared.ViewModels;
using Shopfloor.Stores;

namespace Shopfloor.Features.Login
{
    internal sealed class LoginViewModel : ViewModelBase, IInputForm<User>
    {
        private readonly Dictionary<string, List<string>?> _propertyErrors = [];
        private readonly ICurrentUserStore _userStore;
        private string _username = string.Empty;
        public LoginViewModel(
                    NavigationCommand<MechanicDashboardViewModel> navigationService,
                    ICurrentUserStore userStore)
        {
            ICommand navigateDashboardCommand = navigationService.Navigate();
            _userStore = userStore;
            _userStore.PropertyChanged += OnUserLogin;
            LoginCommand = new LoginCommand(
                _userStore,
                this,
                navigateDashboardCommand);
            //_userValidation = new(this);
        }
        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
        public bool HasErrors => _propertyErrors.Count != 0;
        public bool IsDataValidate => !HasErrors;
        public ICommand LoginCommand { get; }
        public string LoginError
        {
            get
            {
                string myName = nameof(LoginError);
                if (!_propertyErrors.ContainsKey(myName))
                {
                    return string.Empty;
                }

                return _propertyErrors["LoginError"]?[0] ?? string.Empty;
            }
        }
        //private readonly UserValidation _userValidation;
        public string Username
        {
            get => _username;
            set
            {
                string myName = nameof(Username);
                //_userValidation.ValidateName(value, myName);
                _username = value;
                OnPropertyChanged(myName);
            }
        }
        public void AddError(string propertyName, string errorMassage)
        {
            if (!_propertyErrors.TryGetValue(propertyName, out List<string>? value))
            {
                value = [];
                _propertyErrors.Add(propertyName, value);
            }
            value?.Add(errorMassage);

            /*if (!_propertyErrors.ContainsKey(propertyName))
            {
                _propertyErrors.Add(propertyName, []);
            }
            _propertyErrors[propertyName]?.Add(errorMassage);*/
            OnErrorsChanged(propertyName);
            if (propertyName == nameof(LoginError))
            {
                OnPropertyChanged(nameof(LoginError));
            }
        }
        public void CleanForm()
        {
        }
        public void ClearErrors(string? propertyName)
        {
            if (propertyName is null)
            {
                return;
            }

            if (_propertyErrors.Remove(propertyName))
            {
                OnErrorsChanged(propertyName);
            }
            _propertyErrors.Remove("LoginError");
            OnPropertyChanged(nameof(LoginError));
            OnPropertyChanged(nameof(IsDataValidate));
        }
        public IEnumerable GetErrors(string? propertyName)
        {
            return _propertyErrors.GetValueOrDefault(propertyName ?? string.Empty, null) ?? [];
        }
        public void ReloadData()
        {
            throw new NotImplementedException();
        }
        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            OnPropertyChanged(nameof(IsDataValidate));
        }
        private void OnUserLogin(object? sender, PropertyChangedEventArgs e)
        {
            /*if (e.PropertyName == nameof(_userStore.ErrorMassage))
            {
                //OnPropertyChanged(nameof(ErrorMassage));
            }*/
        }
    }
}