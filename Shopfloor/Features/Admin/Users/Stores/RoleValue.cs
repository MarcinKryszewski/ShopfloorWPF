using Shopfloor.Models.RoleModel;

namespace Shopfloor.Features.Admin.Users.Stores
{
    internal sealed class RoleValue
    {
        private readonly Role _role;
        private bool _dirty;
        private bool _value;
        public RoleValue(Role role, bool value)
        {
            _role = role;
            _value = value;
            _dirty = false;
        }
        public bool Dirty => _dirty;
        public Role Role => _role;
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
    }
}