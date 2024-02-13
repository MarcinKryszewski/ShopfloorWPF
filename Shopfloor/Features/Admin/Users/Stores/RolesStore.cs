
using Shopfloor.Models.RoleModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Shopfloor.Features.Admin.Users.Stores
{
    internal sealed class RolesStore
    {
        private readonly ObservableCollection<RoleValue> _roles;

        public ObservableCollection<RoleValue> Roles => _roles;

        public RolesStore()
        {
            _roles = new();
        }

        public void AddRole(Role role, bool assigned)
        {
            _roles.Add(new RoleValue(role, assigned));
        }

        public ObservableCollection<Role> GetAllAssignedRoles()
        {
            List<Role> assignedRoles = _roles
            .Where(role => role.Value == true && role.Dirty == true)
            .Select(role => role.Role)
            .ToList();

            return new ObservableCollection<Role>(assignedRoles);
        }

        public ObservableCollection<Role> GetAllRevokedRoles()
        {
            List<Role> assignedRoles = _roles
            .Where(role => role.Value == false && role.Dirty == true)
            .Select(role => role.Role)
            .ToList();

            return new ObservableCollection<Role>(assignedRoles);
        }

        public void ClearRoles()
        {
            _roles.Clear();
        }
    }
}