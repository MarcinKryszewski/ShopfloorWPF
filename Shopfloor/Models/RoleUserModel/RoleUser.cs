using Shopfloor.Shared.BaseClasses;

namespace Shopfloor.Models.RoleUserModel
{
    internal sealed class RoleUser : DataModel
    {
        private readonly RoleUserDTO _data;
        public required int RoleId
        {
            get => _data.RoleId;
            init => _data.RoleId = value;
        }
        public required int UserId
        {
            get => _data.UserId;
            init => _data.UserId = value;
        }
        public RoleUser()
        {
            _data = new();
        }
    }
}