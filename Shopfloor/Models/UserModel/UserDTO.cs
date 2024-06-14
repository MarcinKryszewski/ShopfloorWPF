using System.Collections.Generic;
using Shopfloor.Models.RoleModel;

namespace Shopfloor.Models.UserModel
{
    internal sealed class UserDTO
    {
        public int? Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public HashSet<Role> Roles { get; set; } = [];
    }
}