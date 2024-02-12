using Shopfloor.Interfaces;
using Shopfloor.Models.ErrandPartModel;
using Shopfloor.Models.ErrandStatusModel;
using Shopfloor.Models.ErrandTypeModel;
using Shopfloor.Models.MachineModel;
using Shopfloor.Models.UserModel;
using System;
using System.Collections.Generic;

namespace Shopfloor.Models.ErrandModel
{
    internal sealed partial class Errand
    {
        public const string DefaultPriority = "C";

        private readonly ErrandDisplay _display;
        private readonly List<ErrandStatus> _errandStatuses = [];
        private readonly List<ErrandPart> _parts = [];
        private User? _createdByUser;
        public Errand(int id, DateTime createdDate, int? createdById, int? machineId, int? errandTypeId, string description, string? sapNumber, DateTime? expectedDate, int? ownerId, string? priority = DefaultPriority)
        {
            _data.Id = id;
            _data.CreatedById = createdById;
            _data.CreatedDate = createdDate;
            _data.OwnerId = ownerId;
            _data.Priority = priority ?? DefaultPriority;
            _data.MachineId = machineId;
            _data.ErrandTypeId = errandTypeId;
            _data.Description = description;
            _data.SapNumber = sapNumber;
            _data.ExpectedDate = expectedDate;
            _display = new(this);
        }
        public Errand(DateTime createdDate, int? createdById, int? machineId, int? errandTypeId, string description, string? priority = DefaultPriority)
        {
            _data.CreatedById = createdById;
            _data.CreatedDate = createdDate;
            _data.MachineId = machineId;
            _data.ErrandTypeId = errandTypeId;
            _data.Description = description;
            _data.Priority = priority ?? DefaultPriority;
            _display = new(this);
        }
        public ErrandDisplay Display => _display;
        public List<ErrandStatus> ErrandStatuses => _errandStatuses;
        public void AddStatus(ErrandStatus status) => _errandStatuses.Add(status);
        public List<ErrandPart> Parts => _parts;
    }
    internal sealed partial class Errand
    {
        private readonly ErrandDTO _data = new();
        public int? CreatedById => _data.CreatedById;
        public User? CreatedByUser
        {
            get => _createdByUser;
            set
            {
                if (value == null) return;
                if (value.Id == _data.CreatedById) _createdByUser = value;
            }
        }
        public DateTime CreatedDate => _data.CreatedDate;
        public string Description
        {
            get => _data.Description;
            set
            {
                if (value == null) return;
                _data.Description = value;
            }
        }
        public int? ErrandTypeId => _data.ErrandTypeId;
        public DateTime? ExpectedDate
        {
            get => _data.ExpectedDate;
            set
            {
                if (value == null) return;
                _data.ExpectedDate = value;
            }
        }
        public int? Id => _data.Id;
        public Machine? Machine
        {
            get => _data.Machine;
            set
            {
                if (value?.Id is not null) _data.MachineId = (int)value.Id;
                _data.Machine = value;
            }
        }
        public int? MachineId => _data.MachineId;
        public int? OwnerId => _data.OwnerId;
        public string Priority
        {
            get => _data.Priority ?? DefaultPriority;
            set => _data.Priority = value;
        }
        public User? Responsible
        {
            get => _data.Responsible;
            set
            {
                if (value?.Id is not null) _data.OwnerId = value.Id;
                _data.Responsible = value;
            }
        }
        public string? SapNumber
        {
            get => _data.SapNumber;
            set => _data.SapNumber = value;
        }
        public ErrandType? Type
        {
            get => _data.ErrandType;
            set
            {
                if (value?.Id is not null) _data.ErrandTypeId = (int)value.Id;
                _data.ErrandType = value;
            }
        }
    }
    internal sealed partial class Errand : ISearchableModel
    {
        public string SearchValue => Machine?.Path ?? string.Empty + Description;
    }
    internal sealed partial class Errand : IEquatable<Errand>
    {
        public bool Equals(Errand? other)
        {
            if (other == null) return false;
            if (Id == null && other.Id == null)
            {
                return CreatedDate == other.CreatedDate;
            }

            return Id == other.Id;
        }
        public override bool Equals(object? obj) => obj is Errand objErrand && Equals(objErrand);
        public override int GetHashCode()
        {
            if (Id != null) return Id.GetHashCode();
            return CreatedDate.GetHashCode();
        }
    }
}