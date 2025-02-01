using System;
using System.Threading.Tasks;

#pragma warning disable S1118 // Utility classes should not have public constructors
namespace Shopfloor.Services.AuthServices
{
    internal class UserProvider
    {
        public static async Task<int?> GetByUsername(string username)
        {
            await Task.Delay(0);
            Random rnd = new();

            return rnd.Next(99, 9999); // test
        }
        public static async Task<string[]> GetRoles(int id)
        {
            await Task.Delay(0);

            return ["admin", "user"]; // test
        }
    }
}
#pragma warning restore S1118 // Utility classes should not have public constructors