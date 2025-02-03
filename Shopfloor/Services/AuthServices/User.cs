using System.Collections.Generic;

namespace Shopfloor.Services.AuthServices
{
    internal class User
    {
        public int Id { get; set; }
        public List<string> Roles { get; } = [];
        public string Username { get; set; } = string.Empty;
    }
}