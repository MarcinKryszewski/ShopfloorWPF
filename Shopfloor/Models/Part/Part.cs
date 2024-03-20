using Shopfloor.Interfaces;
using Shopfloor.Models.PartTypeModel;
using Shopfloor.Models.SupplierModel;
using Shopfloor.Utilities;
using System;

namespace Shopfloor.Models.PartModel
{
    internal sealed partial class Part : DataModel
    {
        private const string _defaultUnit = "szt";
        private PartType? _type;
        private Supplier? _producer;
        private Supplier? _supplier;
        private readonly PartDTO _data;
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
        public double StorageValue
        {
            get => _data.StorageValue;
            set => _data.StorageValue = value;
        }
        public double StorageAmount
        {
            get => _data.StorageAmount;
            set => _data.StorageAmount = value;
        }

        public Part(
            string? namePl, //1
            string? nameOriginal,
            int? typeId,
            int? index, //0
            string? number, //~10
            string? details,
            int? producerId, //~10
            int? supplierId,
            string? unit = _defaultUnit) //3
        {
            _data = new()
            {
                NamePl = namePl ?? string.Empty,
                NameOriginal = nameOriginal ?? string.Empty,
                TypeId = typeId,
                Index = index,
                Number = number ?? string.Empty,
                Details = details ?? string.Empty,
                ProducerId = producerId,
                SupplierId = supplierId,
                Unit = unit ?? _defaultUnit
            };
            SetSearchValue();
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
            string? unit = _defaultUnit)
        {
            _data = new()
            {
                Id = id,
                NamePl = namePl ?? string.Empty,
                NameOriginal = nameOriginal ?? string.Empty,
                TypeId = typeId,
                Index = index,
                Number = number ?? string.Empty,
                Details = details ?? string.Empty,
                ProducerId = producerId,
                SupplierId = supplierId,
                Unit = unit ?? _defaultUnit
            };
            SetSearchValue();
        }
        private string SetInputValue()
        {
            string searchValue = _data.NamePl +
                _data.NameOriginal +
                _data.Index;
            return searchValue;
        }
        public void SetType(PartType? type)
        {
            if (type is null) return;
            _type = type;
            _data.TypeId = type.Id;
        }
        public void SetProducer(Supplier? producer)
        {
            if (producer is null) return;
            _producer = producer;
            _data.ProducerId = producer.Id;
        }
        public void SetSupplier(Supplier? supplier)
        {
            if (supplier is null) return;
            _supplier = supplier;
            _data.SupplierId = supplier.Id;
        }
    }
    internal sealed partial class Part : ISearchableModel
    {
        private string _searchValue = string.Empty;
        public string SearchValue => _searchValue;
        private void SetSearchValue()
        {
            string searchValue = _data.NamePl +
                    _data.NameOriginal +
                    _data.Index.ToString() +
                    _type?.Name ?? string.Empty;
            _searchValue = RemovePolishCharacters.Remove(searchValue.ToLower());
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