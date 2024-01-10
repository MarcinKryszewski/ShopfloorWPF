using Shopfloor.Services.Providers;
using System.Threading.Tasks;

namespace Shopfloor.Models
{
    public class RoleUser
    {
        public int RoleId { get; }
        public int UserId { get; }

        public RoleUser(int roleId, int userId)
        {
            RoleId = roleId;
            UserId = userId;
        }

        public async Task Add(RoleUserProvider provider)
        {
            await provider.Create(this);
        }
        public async Task Edit(RoleUserProvider provider)
        {
            await provider.Update(this);
        }
        public async Task Delete(RoleUserProvider provider)
        {
            await provider.Delete(RoleId, UserId);
        }
    }
}