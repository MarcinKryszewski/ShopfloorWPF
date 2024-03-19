namespace Shopfloor.Models.RoleUserModel
{
    internal sealed class RoleUser : DataModel
    {
        public int? RoleId { get; }
        public int? UserId { get; }

        public RoleUser(int? roleId, int? userId)
        {
            RoleId = roleId;
            UserId = userId;
        }
    }
}