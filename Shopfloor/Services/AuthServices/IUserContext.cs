using System.ComponentModel;
using Shopfloor.Models.Persons;

namespace Shopfloor.Services.AuthServices
{
    internal interface IUserContext : INotifyPropertyChanged
    {
        public bool IsAuthenticated { get; set; }
        public Person? Person { get; set; }
        public User? User { get; set; }
        public string UserPrompt { get; }
        public bool HasRole(string role);
    }
}