using System.ComponentModel;
using Shopfloor.Models.Persons;

namespace Shopfloor.Services.AuthServices
{
    internal interface IUserContext : INotifyPropertyChanged
    {
        public User? User { get; set; }
        public Person? Person { get; set; }
        public bool IsAuthenticated { get; set; }
        public string UserPrompt { get; }
        public bool HasRole(string role);
    }
}