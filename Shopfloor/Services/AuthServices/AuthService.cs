using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopfloor.Services.NotificationServices;

namespace Shopfloor.Services.AuthServices
{
    internal class AuthService
    {
        private readonly UserContext _userContext;
        private readonly INotifier _notifier;
        private readonly UserProvider _provider;

        public AuthService(
            UserContext userContext,
            INotifier notifier,
            UserProvider provider)
        {
            _userContext = userContext;
            _notifier = notifier;
            _provider = provider;
        }
        public UserContext GetUserContext() => _userContext;
        public async Task Login(string username)
        {
            int? userId = await _provider.GetByUsername(username);

            if (userId == null)
            {
                await Logout();
                return;
            }

            User user = new()
            {
                Username = username,
                Id = (int)userId,
            };
            user.Roles.AddRange(await _provider.GetRoles((int)userId));

            _userContext.User = user;
            _userContext.IsAuthenticated = true;
        }

        public Task Logout()
        {
            _userContext.User = null;
            _userContext.IsAuthenticated = false;

            return Task.CompletedTask;
        }
    }
}