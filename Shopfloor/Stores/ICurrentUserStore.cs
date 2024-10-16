using System.ComponentModel;
using Shopfloor.Models.UserModel;

namespace Shopfloor.Stores
{
    internal interface ICurrentUserStore : INotifyDataErrorInfo, INotifyPropertyChanged
    {
        public bool IsUserLoggedIn { get; }
        public User? User { get; }
        public void Login(string username, bool isAuto = false);
        public void Logout();
    }
}