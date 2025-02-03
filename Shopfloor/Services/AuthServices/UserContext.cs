using Shopfloor.Models.Persons;
using Shopfloor.Shared;

namespace Shopfloor.Services.AuthServices
{
    internal class UserContext : ObservableObject, IUserContext
    {
        private bool _isAuthenticated = false;
        private Person? _person;
        public bool IsAuthenticated
        {
            get => _isAuthenticated;
            set
            {
                _isAuthenticated = value;
                OnPropertyChanged(nameof(IsAuthenticated));
            }
        }
        public Person? Person
        {
            get => _person;
            set
            {
                _person = value;
                OnPropertyChanged(nameof(Person));
            }
        }
        public User? User { get; set; }
        public string UserPrompt
        {
            get
            {
                if (!IsAuthenticated)
                {
                    string loginPrompt = "Witaj! Jesteś niezalogowany.";
                    return loginPrompt;
                }

                if (Person is null)
                {
                    string personNotFoundInDatabase = "Dopisz się do listy osób!";
                    return personNotFoundInDatabase;
                }

                return $"Witaj {Person.Name}!";
            }
        }
        public bool HasRole(string role) => User?.Roles.Contains(role) ?? false;
    }
}