using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Interfaces;

namespace Shopfloor.Models.UserModel
{
    public class UserStore : IDataStore<User>
    {
        private readonly IServiceProvider _databaseServices;
        private IEnumerable<User> _data = [];
        public UserStore(IServiceProvider databaseServices)
        {
            _databaseServices = databaseServices;
        }
        public IEnumerable<User> Data => _data;
        public bool IsLoaded { get; private set; }
        public Task Load()
        {
            UserProvider provider = _databaseServices.GetRequiredService<UserProvider>();
            _data = provider.GetAll().Result;
            IsLoaded = true;
            return Task.CompletedTask;
        }
        public async Task Reload()
        {
            UserProvider provider = _databaseServices.GetRequiredService<UserProvider>();
            _data = await provider.GetAll();
        }
    }
}