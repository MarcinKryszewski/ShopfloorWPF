using Shopfloor.Services.Providers;
using System.Threading.Tasks;

namespace Shopfloor.Models
{
    public class PartType
    {
        private readonly int _id;
        private string _name;

        public int Id => _id;
        public string Name => _name;

        public PartType(int id, string name)
        {
            _id = id;
            _name = name;
        }

        public PartType(string name)
        {
            _name = name;
        }

        public async Task Add(PartTypeProvider provider)
        {
            await provider.Create(this);
        }
        public async Task Edit(PartTypeProvider provider)
        {
            await provider.Update(this);
        }
        public async Task Delete(PartTypeProvider provider)
        {
            await provider.Delete(Id);
        }
    }
}