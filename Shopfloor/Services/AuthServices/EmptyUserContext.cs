using System.ComponentModel;
using Shopfloor.Models.Persons;

namespace Shopfloor.Services.AuthServices
{
    internal class EmptyUserContext : IUserContext
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public bool IsAuthenticated
        {
            get => false;
            set { }
        }
        public Person? Person
        {
            get => null;
            set { }
        }
        public User? User
        {
            get => null;
            set { }
        }
        public string UserPrompt => string.Empty;
        public bool HasRole(string role) => false;
    }
}