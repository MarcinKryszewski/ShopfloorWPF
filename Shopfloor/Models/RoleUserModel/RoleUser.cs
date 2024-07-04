using Shopfloor.Shared.BaseClasses;

namespace Shopfloor.Models.RoleUserModel
{
    internal sealed class RoleUser : DataModel
    {
        private readonly RoleUserDto _data;
        public RoleUser()
        {
            _data = new();
        }
        required public int RoleId
        {
            get => _data.RoleId;
            init => _data.RoleId = value;
        }
        required public int UserId
        {
            get => _data.UserId;
            init => _data.UserId = value;
        }
    }
}