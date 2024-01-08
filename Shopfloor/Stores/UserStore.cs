using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;

namespace Shopfloor.Stores
{
    public class UserStore
    {
        private string _username;
        private string _name;
        private readonly string? _surname;
        private readonly List<int> _permissions;
        private readonly string _image;
        private Boolean _isUserLoggedIn;

        public string Username
        {
            get => _username;
        }
        public string? Name => _name;
        public string? Surname => _surname;
        public string Image => _image;
        public Boolean IsUserLoggedIn => _isUserLoggedIn;

        public UserStore()
        {
            _username = "GOŚĆ";
            _name = "";
            _surname = "";
            _permissions = new();
            _image = "pack://application:,,,/Shopfloor;component/Resources/userDefault.png";
            _isUserLoggedIn = false;
        }

        public void AddPermission(int permissionValue)
        {
            _permissions.Add(permissionValue);
        }

        public bool HasUserPermission(int permissionValue)
        {
            return _permissions.Contains(permissionValue);
        }


    }
}