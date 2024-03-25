using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Interfaces;
using Shopfloor.Models.UserModel.Store.Combine;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopfloor.Models.UserModel
{
    internal sealed class UserStore : StoreBase<User>
    {
        public UserStore(UserProvider provider, UserCombiner combiner) : base(provider, combiner)
        {

        }
    }
}