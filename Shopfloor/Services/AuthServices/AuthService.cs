using Shopfloor.Models.UserModel;

namespace Shopfloor.Services
{
    internal sealed class AuthService : IAuthService
    {
        private readonly UserProvider _provider;
        public AuthService(UserProvider provider)
        {
            _provider = provider;
        }
        public User? Login(string username)
        {
            return _provider.GetByUsername(username.ToLower()).Result ?? null;
        }
    }

    internal interface IAuthService
    {
        public User? Login(string username);
    }
}