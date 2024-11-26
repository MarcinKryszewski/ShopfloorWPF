using System;
using System.Threading.Tasks;

namespace Shopfloor.Services.AuthServices
{
    internal class UserProvider
    {
        public async Task<int?> GetByUsername(string username)
        {
            await Task.Delay(0);
            Random rnd = new();

            return rnd.Next(99, 9999); // test
        }
        public async Task<string[]> GetRoles(int id)
        {
            await Task.Delay(0);

            return ["admin", "user"]; // test
        }
    }
}