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
        //private Dictionary<Role, bool> _rolesAssign;
        private ObservableCollection<RoleValue> _roles;

        public ObservableCollection<RoleValue> Roles => _roles;

        public RolesStore()
        {
            //_rolesAssign = new();
            _roles = new();
        }

        public void AddRole(Role role, bool assigned)
        {
            //_rolesAssign.Add(role, assigned);
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
    }

    public class RoleValue
    {
        private readonly Role _role;
        private bool _value;
        private bool _dirty;

        public Role Role => _role;
        public bool Dirty => _dirty;
        public bool Value
        {
            get => _value;
            set
            {
                if (value != _value)
                {
                    _value = value;
                    _dirty = true;
                }

            }
        }

        public RoleValue(Role role, bool value)
        {
            _role = role;
            _value = value;
            _dirty = false;
        }
    }
}