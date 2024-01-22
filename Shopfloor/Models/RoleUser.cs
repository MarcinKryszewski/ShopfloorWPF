using Shopfloor.Services.Providers;
using System.Threading.Tasks;

namespace Shopfloor.Models
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