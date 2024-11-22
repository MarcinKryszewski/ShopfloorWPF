using System.Collections.Generic;
using Shopfloor.Models.Persons;

namespace Shopfloor.Services.AuthServices
{
    internal class UserContext
    {
        public User? User { get; set; }
        public Person? Person { get; set; }
        public bool IsAuthenticated { get; set; } = false;
        public bool HasRole(string role) => User?.Roles.Contains(role) ?? false;
    }
}