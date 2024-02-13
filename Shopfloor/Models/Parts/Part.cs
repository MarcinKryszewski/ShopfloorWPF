using Shopfloor.Interfaces;
using Shopfloor.Models.PartTypeModel;
using Shopfloor.Models.SupplierModel;
using System;

namespace Shopfloor.Models.PartModel
{
    internal sealed partial class Part
    {
        private const string _defaultUnit = "SZT";
        private PartType? _type;
        private Supplier? _producer;
        private Supplier? _supplier;
        private readonly PartDTO _data = new();
        public int? Id => _data.Id;
        public string NamePl => _data.NamePl;
        public string NameOriginal => _data.NameOriginal;
        public int? TypeId => _data.TypeId;
        public PartType? Type => _type;
        public string TypeName => _type?.Name ?? string.Empty;
        public int? Index => _data.Index;
        public string Number => _data.Number;
        public string Details => _data.Details;
        public int? ProducerId => _data.ProducerId;
        public Supplier? Producer => _producer;
        public int? SupplierId => _data.SupplierId;
        public Supplier? Supplier => _supplier;
        public string Unit => _data.Unit;
        public string RequiredInputValue => SetInputValue();
        public Part(
            string? namePl,
            string? nameOriginal,
            int? typeId,
            int? index,
            string? number,
            string? details,
            int? producerId,
            int? supplierId,
            string unit = _defaultUnit)
        {
            _data.NamePl = namePl ?? string.Empty;
            _data.NameOriginal = nameOriginal ?? string.Empty;
            _data.TypeId = typeId;
            _data.Index = index;
            _data.Number = number ?? string.Empty;
            _data.Details = details ?? string.Empty;
            _data.ProducerId = producerId;
            _data.SupplierId = supplierId;
            _data.Unit = unit;
        }
        public Part(
            int? id,
            string? namePl,
            string? nameOriginal,
            int? typeId,
            int? index,
            string? number,
            string? details,
            int? producerId,
            int? supplierId,
            string unit = _defaultUnit)
        {
            _data.Id = id;
            _data.NamePl = namePl ?? string.Empty;
            _data.NameOriginal = nameOriginal ?? string.Empty;
            _data.TypeId = typeId;
            _data.Index = index;
            _data.Number = number ?? string.Empty;
            _data.Details = details ?? string.Empty;
            _data.ProducerId = producerId;
            _data.SupplierId = supplierId;
            _data.Unit = unit;
        }
        private string SetInputValue()
        {
            string searchValue = _data.NamePl +
                _data.NameOriginal +
                _data.Index;
            return searchValue;
        }
        public void SetType(PartType type)
        {
            if (type is null) return;
            _type = type;
            _data.TypeId = type.Id;
        }
        public void SetProducer(Supplier producer)
        {
            if (producer is null) return;
            _producer = producer;
            _data.ProducerId = producer.Id;
        }
        public void SetSupplier(Supplier supplier)
        {
            if (supplier is null) return;
            _supplier = supplier;
            _data.SupplierId = supplier.Id;
        }
    }
    internal sealed partial class Part : ISearchableModel
    {
        public string SearchValue
        {
            get
            {
                string searchValue = _data.NamePl +
                    _data.NameOriginal +
                    (_type?.Name ?? string.Empty) +
                    _data.Index +
                    _data.Details +
                    (_producer?.Name ?? string.Empty) +
                    (_supplier?.Name ?? string.Empty);
                return searchValue;
            }
        }
    }
    internal sealed partial class Part : IEquatable<Part>
    {
        public bool Equals(Part? other)
        {
            if (other == null) return false;
            return _data.Id == other.Id;
        }
        public override bool Equals(object? obj) => obj is Part objPart && Equals(objPart);
        public override int GetHashCode()
        {
            if (Id != null) return Id.GetHashCode();
            return SearchValue.GetHashCode();
        }
    }
}