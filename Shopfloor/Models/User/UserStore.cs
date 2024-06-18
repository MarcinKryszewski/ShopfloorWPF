using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopfloor.Models.UserModel
{
    internal sealed class UserStore : IDataStore<User>
    {
        private readonly IServiceProvider _databaseServices;
        private List<User> _data = [];
        public UserStore(IServiceProvider databaseServices)
        {
            _databaseServices = databaseServices;
        }
        public List<User> Data => _data;
        public bool IsLoaded { get; private set; }

        public Task CombineData()
        {
            throw new NotImplementedException();
        }

        public Task Load()
        {
            UserProvider provider = _databaseServices.GetRequiredService<UserProvider>();
            _data = new(provider.GetAll().Result);
            IsLoaded = true;
            return Task.CompletedTask;
        }
        public async Task Reload()
        {
            UserProvider provider = _databaseServices.GetRequiredService<UserProvider>();
            _data = new(await provider.GetAll());
        }
    }
}