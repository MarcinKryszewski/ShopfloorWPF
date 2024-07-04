using System.Collections.Generic;
using Shopfloor.Models.RoleModel;

namespace Shopfloor.Models.UserModel
{
    internal sealed class UserDto
    {
        public int? Id { get; set; }
        public string ImagePath { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public string Name { get; set; } = string.Empty;
        public HashSet<Role> Roles { get; set; } = [];
        public string Surname { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
    }
}