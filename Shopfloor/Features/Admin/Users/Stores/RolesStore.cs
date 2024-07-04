using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Shopfloor.Models.RoleModel;

namespace Shopfloor.Features.Admin.Users.Stores
{
    internal sealed class RolesStore
    {
        private readonly ObservableCollection<RoleValue> _roles;
        public RolesStore()
        {
            _roles = [];
        }
        public ObservableCollection<RoleValue> Roles => _roles;
        public void AddRole(Role role, bool assigned)
        {
            _roles.Add(new RoleValue(role, assigned));
        }
        public void ClearRoles()
        {
            _roles.Clear();
        }
        public ObservableCollection<Role> GetAllAssignedRoles()
        {
            List<Role> assignedRoles = _roles
            .Where(role => role.Value && role.Dirty)
            .Select(role => role.Role)
            .ToList();

            return new ObservableCollection<Role>(assignedRoles);
        }
        public ObservableCollection<Role> GetAllRevokedRoles()
        {
            List<Role> assignedRoles = _roles
            .Where(role => !role.Value && role.Dirty)
            .Select(role => role.Role)
            .ToList();

            return new ObservableCollection<Role>(assignedRoles);
        }
    }
}