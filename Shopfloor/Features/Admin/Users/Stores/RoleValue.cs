

using Shopfloor.Models.RoleModel;

namespace Shopfloor.Features.Admin.Users.Stores
{
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