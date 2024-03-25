using System.Threading.Tasks;
using Shopfloor.Interfaces;

namespace Shopfloor.Models.MessageModel.Store.Combine
{
    public class MessageCombiner : ICombiner
    {
        public Task Combine()
        {
            return Task.CompletedTask;
        }
    }
}