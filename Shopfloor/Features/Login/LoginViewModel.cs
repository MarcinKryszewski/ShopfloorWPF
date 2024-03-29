using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.Dashboard;
using Shopfloor.Features.Login.Commands;
using Shopfloor.Interfaces;
using Shopfloor.Models.UserModel;
using Shopfloor.Shared.Commands;
using Shopfloor.Shared.Services;
using Shopfloor.Shared.ViewModels;
using Shopfloor.Stores;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using ToastNotifications;

namespace Shopfloor.Features.Login
{
    internal sealed class LoginViewModel : ViewModelBase, IInputForm<User>
    {
        private string _username = string.Empty;
        private readonly CurrentUserStore _userStore;
        private readonly UserValidation _userValidation;
        public string Username
        {
            get => _username;
            set
            {
                string myName = nameof(Username);
                _userValidation.ValidateName(value, myName);
                _username = value;
                OnPropertyChanged(myName);
            }
        }
        public string LoginError
        {
            get
            {
                string myName = nameof(LoginError);
                if (!_propertyErrors.ContainsKey(myName)) return string.Empty;
                return _propertyErrors["LoginError"]?[0] ?? string.Empty;
            }
        }

        public ICommand LoginCommand { get; }
        public LoginViewModel(IServiceProvider mainServices, IServiceProvider databaseServices, IServiceProvider userProvider)
        {
            ICommand NavigateDashboardCommand = new NavigateCommand<DashboardViewModel>(mainServices.GetRequiredService<NavigationService<DashboardViewModel>>());
            _userStore = userProvider.GetRequiredService<CurrentUserStore>();
            _userStore.PropertyChanged += OnUserLogin;
            LoginCommand = new LoginCommand(
                databaseServices.GetRequiredService<UserProvider>(),
                _userStore,
                this,
                NavigateDashboardCommand,
                mainServices.GetRequiredService<Notifier>());
            _userValidation = new(this);
        }
        private void OnUserLogin(object? sender, PropertyChangedEventArgs e)
        {
            /*if (e.PropertyName == nameof(_userStore.ErrorMassage))
            {
                //OnPropertyChanged(nameof(ErrorMassage));
            }*/
        }
        public void CleanForm()
        {
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
        public void ClearErrors(string? propertyName)
        {
            if (propertyName is null) return;
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
            if (propertyName == nameof(LoginError)) OnPropertyChanged(nameof(LoginError));
        }
        public bool HasErrors => _propertyErrors.Count != 0;
        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
        private readonly Dictionary<string, List<string>?> _propertyErrors = [];
        public bool IsDataValidate => !HasErrors;
    }
}