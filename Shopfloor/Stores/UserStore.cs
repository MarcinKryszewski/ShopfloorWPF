using Shopfloor.Models;

namespace Shopfloor.Stores
{
    public class UserStore
    {
        private User _user;
        private bool _isUserLoggedIn;

        public bool IsUserLoggedIn => _isUserLoggedIn;
        public User User => _user;

        public UserStore()
        {
            _user = new("GOŚĆ");
            _isUserLoggedIn = false;
        }

        public void Login(User user)
        {
            _user = user;
            _isUserLoggedIn = true; 
        }
        public void Logout()
        {
            _user = new("GOŚĆ");
            _isUserLoggedIn = false;
        }
    }
}