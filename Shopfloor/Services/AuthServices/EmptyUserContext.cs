using System;
using System.ComponentModel;
using Shopfloor.Models.Persons;

namespace Shopfloor.Services.AuthServices
{
    internal class EmptyUserContext : IUserContext
    {
        public event PropertyChangedEventHandler? PropertyChanged
        {
            add { throw new NotSupportedException(); }
            remove { throw new NotSupportedException(); }
        }
#pragma warning disable S108 // Nested blocks of code should not be left empty
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
#pragma warning restore S108 // Nested blocks of code should not be left empty
        public string UserPrompt => string.Empty;
        public bool HasRole(string role) => false;
    }
}