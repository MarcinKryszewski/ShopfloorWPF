using Shopfloor.Interfaces;
using Shopfloor.Models.PartTypeModel;
using Shopfloor.Models.SupplierModel;
using Shopfloor.Shared;
using Shopfloor.Shared.BaseClasses;
using Shopfloor.Utilities;
using System;

namespace Shopfloor.Models.PartModel
{
    internal sealed partial class Part : DataModel
    {
        private readonly PartDTO _data;
        public Part()
        {
            _data = new();
        }
        public int? Id
        {
            get => _data.Id;
            init => _data.Id = value;
        }
        public string? NamePl
        {
            get => _data.NamePl;
            set => _data.NamePl = value;
        }
        public required string NameOriginal
        {
            get => _data.NameOriginal ?? string.Empty;
            set => _data.NameOriginal = value;
        }
        public required int TypeId
        {
            get => _data.TypeId;
            set => _data.TypeId = value;
        }
        public int? Index
        {
            get => _data.Index;
            set => _data.Index = value;
        }
        public string ProducerNumber
        {
            get => _data.Number ?? string.Empty;
            set => _data.Number = value;
        }
        public string? Details
        {
            get => _data.Details ?? string.Empty;
            set => _data.Details = value;
        }
        public int ProducerId
        {
            get => _data.ProducerId;
            init => _data.ProducerId = value;
        }
        public int? SupplierId
        {
            get => _data.SupplierId;
            set => _data.SupplierId = value;
        }
        public string? Unit
        {
            get => _data.Unit;
            set => _data.Unit = value ?? GlobalConstants.DefaultPartUnit;
        }
        public double? StorageAmount
        {
            get => _data.StorageAmount;
            set => _data.StorageAmount = value ?? 0;
        }
        public double? StorageValue
        {
            get => _data.StorageValue;
            set => _data.StorageValue = value ?? 0;
        }
        public PartType? PartType
        {
            get => _data.PartType;
            set
            {
                _data.PartType = value;
                _data.TypeId = value?.Id ?? 0;
            }
        }
        public Supplier? Producer
        {
            get => _data.Producer;
            set
            {
                _data.Producer = value;
                _data.ProducerId = value?.Id ?? 0;
            }
        }
        public Supplier? Supplier
        {
            get => _data.Supplier;
            set
            {
                _data.Supplier = value;
                _data.SupplierId = value?.Id ?? 0;
            }
        }
    }
    internal sealed partial class Part : ISearchableModel
    {
        public string SearchValue => GetSearchValue();
        private string GetSearchValue()
        {
            string searchValue = _data.NamePl +
                    _data.NameOriginal +
                    _data.Index.ToString() +
                    PartType?.Name ?? string.Empty;
            return RemovePolishCharacters.Remove(searchValue.ToLower());
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