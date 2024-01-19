using Shopfloor.Interfaces;
using Shopfloor.Services.Providers;
using System;
using System.Threading.Tasks;

namespace Shopfloor.Models
{
    public class PartType : IEquatable<PartType>, ISearchableModel
    {
        private readonly int? _id;
        private string _name;

        public int? Id => _id;
        public string Name => _name;
        public string SearchValue => _name ?? string.Empty;

        public PartType(int id, string name)
        {
            _id = id;
            _name = name;
        }

        public PartType(string name)
        {
            _name = name;
        }

        public bool Equals(PartType? other)
        {
            if (other == null) return false;
            return _id == other.Id;
        }
    }
}