using Shopfloor.Models.UserModel;

namespace Shopfloor.Services
{
    internal interface IAuthService
    {
        public User? Login(string username);
    }
}