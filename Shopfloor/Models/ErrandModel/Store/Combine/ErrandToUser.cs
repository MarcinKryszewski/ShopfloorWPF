﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Shopfloor.Interfaces;
using Shopfloor.Models.UserModel;

namespace Shopfloor.Models.ErrandModel.Store.Combine
{
    internal sealed class ErrandToUser : ICombiner<Errand>
    {
        private readonly IDataStore<Errand> _errandStore;
        private readonly IDataStore<User> _userStore;
        public ErrandToUser(IDataStore<User> userStore, IDataStore<Errand> errandStore)
        {
            _userStore = userStore;
            _errandStore = errandStore;
        }
        public Task CombineAll()
        {
            List<Errand> errands = GetErrands();
            List<User> users = GetUsers();

            foreach (Errand item in errands)
            {
                Combine(item, users);
            }
            return Task.CompletedTask;
        }
        public Task CombineOne(Errand item)
        {
            List<User> users = GetUsers();

            Combine(item, users);

            return Task.CompletedTask;
        }
        private static void Combine(Errand errand, List<User> users)
        {
            errand.CreatedByUser = users.Find(user => user.Id == errand.CreatedById);
            errand.Responsible = users.Find(user => user.Id == errand.OwnerId);
        }
        private List<Errand> GetErrands() => _errandStore.Data;
        private List<User> GetUsers() => _userStore.Data;
    }
}