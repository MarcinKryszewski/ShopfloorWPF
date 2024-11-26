using Shopfloor.Models.Persons;
using Shopfloor.Shared;

namespace Shopfloor.Services.AuthServices
{
    internal class UserContext : ObservableObject, IUserContext
    {
        private Person? _person;
        public User? User { get; set; }
        public Person? Person
        {
            get => _person;
            set
            {
                _person = value;
                OnPropertyChanged(nameof(Person));
            }
        }
        public bool IsAuthenticated { get; set; } = false;
        public string UserPrompt => Person is null ? "Witaj! Jesteś niezalogowany." : $"Witaj {Person.Name}!";
        public bool HasRole(string role) => User?.Roles.Contains(role) ?? false;
    }
}