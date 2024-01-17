using System;
using Shopfloor.Interfaces;

namespace Shopfloor.Models
{
    public class Part : IEquatable<Part>, ISearchableModel
    {
        private readonly int _id;
        private string? _namePl;
        private string? _nameOriginal;
        private PartType? _type;
        private int? _index;
        private string? _number;
        private string? _details;
        private Supplier? _producer;
        private Supplier? _supplier;

        public int Id => _id;
        public string NamePl => _namePl ?? string.Empty;
        public string NameOriginal => _nameOriginal ?? string.Empty;
        public PartType? Type => _type;
        public int? Index => _index;
        public string Number => _number ?? string.Empty;
        public string Details => _details ?? string.Empty;
        public Supplier? Producer => _producer;
        public Supplier? Supplier => _supplier;

        public string SearchValue => SetSearchValue();

        public Part(
            string? namePl,
            string? nameOriginal,
            PartType? type,
            int? index,
            string? number,
            string? details,
            Supplier? producer,
            Supplier? supplier)
        {
            _namePl = namePl;
            _nameOriginal = nameOriginal;
            _type = type;
            _index = index;
            _number = number;
            _details = details;
            _producer = producer;
            _supplier = supplier;
        }

        public Part(
            int id,
            string?
            namePl,
            string? nameOriginal,
            PartType? type,
            int? index,
            string? number,
            string? details,
            Supplier? producer,
            Supplier? supplier)
        {
            _id = id;
            _namePl = namePl;
            _nameOriginal = nameOriginal;
            _type = type;
            _index = index;
            _number = number;
            _details = details;
            _producer = producer;
            _supplier = supplier;
        }

        private string SetSearchValue()
        {
            return (
                _namePl ?? string.Empty +
                _nameOriginal ?? string.Empty +
                _type?.Name ?? string.Empty +
                _index ?? string.Empty +
                _details ?? string.Empty +
                _producer?.Name ?? string.Empty +
                _supplier?.Name ?? string.Empty
            );
        }

        public bool Equals(Part? other)
        {
            if (other == null) return false;
            return _id == other.Id;
        }
    }
}