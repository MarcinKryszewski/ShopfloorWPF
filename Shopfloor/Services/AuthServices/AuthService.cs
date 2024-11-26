using System;
using System.Threading.Tasks;
using System.Linq;
using Shopfloor.Models.Commons.Interfaces;
using Shopfloor.Models.Persons;
using Shopfloor.Services.NotificationServices;

namespace Shopfloor.Services.AuthServices
{
    internal class AuthService
    {
        private readonly IUserContext _userContext;
        private readonly INotifier _notifier;
        private readonly UserProvider _userProvider;
        private readonly IRepository<Person, PersonCreation> _personRepository;
        public AuthService(
            IUserContext userContext,
            INotifier notifier,
            UserProvider userprovider,
            IRepository<Person, PersonCreation> personRepository)
        {
            _userContext = userContext;
            _notifier = notifier;
            _userProvider = userprovider;
            _personRepository = personRepository;
        }
        public IUserContext GetUserContext() => _userContext;
        public async Task AutoLogin()
        {
            string username = Environment.UserName;
            await Login(username);
        }
        public async Task Login(string username)
        {
            int? userId = await _userProvider.GetByUsername(username);

            if (userId == null)
            {
                await Logout();
                NotifyFailedLogin();
                return;
            }

            _userContext.Person = await GetPerson(username);

            User user = new()
            {
                Username = username,
                Id = (int)userId,
            };
            user.Roles.AddRange(await _userProvider.GetRoles((int)userId));

            _userContext.User = user;
            _userContext.IsAuthenticated = true;
            NotifySuccessfulLogin();
        }
        public Task Logout()
        {
            _userContext.User = null;
            _userContext.IsAuthenticated = false;
            NotifyLogout();

            return Task.CompletedTask;
        }
        private void NotifyLogout()
        {
            string logoutInfoText = "Wylogowano";
            _notifier.ShowInformation(logoutInfoText);
        }
        private void NotifySuccessfulLogin()
        {
            string loginInfoText = "Zalogowano";
            _notifier.ShowSuccess(loginInfoText);
        }
        private void NotifyFailedLogin()
        {
            string loginInfoText = "Niestety, nie zalogowano";
            _notifier.ShowSuccess(loginInfoText);
        }
        private async Task<Person> GetPerson(string username)
        {
            return (await _personRepository.GetDataAsync()).First(x => x.Username == username);
        }
    }
}