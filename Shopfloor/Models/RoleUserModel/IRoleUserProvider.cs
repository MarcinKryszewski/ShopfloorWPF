using System.Collections.Generic;
using System.Threading.Tasks;
using Shopfloor.Interfaces;

namespace Shopfloor.Models.RoleUserModel
{
    internal interface IRoleUserProvider : IProvider<RoleUser>
    {
        Task<IEnumerable<RoleUser>> GetAllForUser(int userId);
        Task<int> Create(int roleId, int userId);
        Task Delete(int roleId, int userId);
    }
}