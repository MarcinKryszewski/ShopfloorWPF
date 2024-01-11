using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Shopfloor.Models;

namespace Shopfloor.Features.Admin.Users.Stores
{
    public class RolesStore
    {
        private Dictionary<Role, bool> _rolesAssign;
        private ObservableCollection<RoleValue> _roles;

        public ObservableCollection<RoleValue> Roles => _roles;

        public RolesStore()
        {
            _rolesAssign = new();
            _roles = new();
        }

        public void AddRole(Role role, bool assigned)
        {
            _rolesAssign.Add(role, assigned);
            _roles.Add(new RoleValue(role, assigned));
        }

        public ObservableCollection<Role> GetAllAssignedRoles()
        {
            List<Role> assignedRoles = _rolesAssign
            .Where(role => role.Value == true)
            .Select(role => role.Key)
            .ToList();

            return new ObservableCollection<Role>(assignedRoles);
        }
    }

    public class RoleValue
    {
        private Role? _role;
        private bool _value;

        public Role? Role => _role;
        public bool Value
        {
            get => _value;
            set
            {
                _value = value;
            }
        }

        public RoleValue(Role role, bool value)
        {
            _role = role;
            _value = value;
        }
    }
}