using System.Threading.Tasks;
using Shopfloor.Interfaces;

namespace Shopfloor.Models.UserModel
{
    internal interface IUserProvider : IProvider<User>
    {
        public Task<User?> GetByUsername(string username);
        public Task SetUserActive(int id, bool isActive);
    }
}