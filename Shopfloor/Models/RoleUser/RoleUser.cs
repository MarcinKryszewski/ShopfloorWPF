
using System.Threading.Tasks;

namespace Shopfloor.Models.RoleUserModel
{
    public class RoleUser
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