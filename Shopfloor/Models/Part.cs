using Shopfloor.Interfaces;
using System;

namespace Shopfloor.Models
{
    public class Part : IEquatable<Part>, ISearchableModel
    {
        private readonly int _id;
        private string _namePl = string.Empty;
        private string _nameOriginal = string.Empty;
        private int? _typeId;
        private PartType? _type;
        private int? _index;
        private string _number = string.Empty;
        private string _details = string.Empty;
        private int? _producerId;
        private Supplier? _producer;
        private int? _supplierId;
        private Supplier? _supplier;

        public int Id => _id;
        public string NamePl => _namePl;
        public string NameOriginal => _nameOriginal;
        public int? TypeId => _typeId;
        public PartType? Type => _type;
        public string TypeName => _type?.Name ?? string.Empty;
        public int? Index => _index;
        public string Number => _number;
        public string Details => _details;
        public int? ProducerId => _producerId;
        public Supplier? Producer => _producer;
        public int? SupplierId => _supplierId;
        public Supplier? Supplier => _supplier;

        public string SearchValue => SetSearchValue();
        public string RequiredInputValue => SetInputValue();

        public Part(
            string? namePl,
            string? nameOriginal,
            int? typeId,
            int? index,
            string? number,
            string? details,
            int? producerId,
            int? supplierId)
        {
            _namePl = namePl ?? string.Empty;
            _nameOriginal = nameOriginal ?? string.Empty;
            _typeId = typeId;
            _index = index;
            _number = number ?? string.Empty;
            _details = details ?? string.Empty;
            _producerId = producerId;
            _supplierId = supplierId;
        }

        public Part(
            int id,
            string? namePl,
            string? nameOriginal,
            int? typeId,
            int? index,
            string? number,
            string? details,
            int? producerId,
            int? supplierId)
        {
            _id = id;
            _namePl = namePl ?? string.Empty;
            _nameOriginal = nameOriginal ?? string.Empty;
            _typeId = typeId;
            _index = index;
            _number = number ?? string.Empty;
            _details = details ?? string.Empty;
            _producerId = producerId;
            _supplierId = supplierId;
        }

        private string SetSearchValue()
        {
            string searchValue = _namePl +
                _nameOriginal +
                (_type?.Name ?? string.Empty) +
                _index +
                _details +
                (_producer?.Name ?? string.Empty) +
                (_supplier?.Name ?? string.Empty);
            return searchValue;
        }
        private string SetInputValue()
        {
            string searchValue = _namePl +
                _nameOriginal +
                _index;
            return searchValue;
        }

        public bool Equals(Part? other)
        {
            if (other == null) return false;
            return _id == other.Id;
        }

        public void SetType(PartType type)
        {
            if (type is null) return;
            _type = type;
            _typeId = type.Id;
        }
        public void SetProducer(Supplier producer)
        {
            if (producer is null) return;
            _producer = producer;
            _producerId = producer.Id;
        }
        public void SetSupplier(Supplier supplier)
        {
            if (supplier is null) return;
            _supplier = supplier;
            _supplierId = supplier.Id;
        }
    }
}