namespace Shopfloor.Models.RoleUserModel
{
    internal sealed class RoleUser : DataModel
    {
        private RoleUserDTO _data => new();
        public required int RoleId
        {
            get => _data.Role_Id;
            init => _data.Role_Id = value;
        }
        public required int UserId
        {
            get => _data.User_Id;
            init => _data.User_Id = value;
        }
    }
}