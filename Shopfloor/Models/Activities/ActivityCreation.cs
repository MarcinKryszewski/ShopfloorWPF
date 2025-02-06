using Shopfloor.Models.ActivityTypes;
using Shopfloor.Models.Commons.BaseClasses;
using Shopfloor.Models.Commons.Interfaces;
using Shopfloor.Models.Machines;
using Shopfloor.Models.Workshops;

namespace Shopfloor.Models.Activities
{
    internal class ActivityCreation : ModelValidationBase, IModelCreationModel<Activity>
    {
        private Machine? _machine;
        private ActivityType? _type;
        private Workshop? _workshop;
        public ActivityCreation(Activity model)
        {
            Id = model.Id;
            MachineId = model.MachineId;
            Machine = model.Machine;
            WorkshopId = model.WorkshopId;
            Workshop = model.Workshop;
            Status = model.Status;
            Description = model.Description;
            TypeId = model.TypeId;
            Type = model.Type;
            IsLoto = model.IsLoto;
            IsJog = model.IsJog;
            IsDurningProduction = model.IsDurningProduction;
            OccuranceValue = model.OccuranceValue;
            Occurance = model.Occurance;
        }
        public ActivityCreation()
        {
        }
        public string Description { get; set; } = string.Empty;
        public int Id { get; set; }
        public bool IsDurningProduction { get; set; }
        public bool IsJog { get; set; }
        public bool IsLoto { get; set; }
        public Machine? Machine
        {
            get => _machine;
            set
            {
                _machine = value;
                MachineId = value?.Id ?? -1;
            }
        }
        public int MachineId { get; set; }
        public Occurance Occurance { get; set; } = new();
        public int OccuranceValue { get; set; } = 1;
        public ActivityStatus Status { get; set; } = ActivityStatus.Unconfirmed;
        public string StatusText => Status.ToString();
        public ActivityType? Type
        {
            get => _type;
            set
            {
                _type = value;
                TypeId = value?.Id ?? -1;
            }
        }
        public int TypeId { get; set; }
        public Workshop? Workshop
        {
            get => _workshop;
            set
            {
                _workshop = value;
                WorkshopId = value?.Id ?? -1;
            }
        }
        public int WorkshopId { get; set; }
        public Activity CreateModel(int id)
        {
            return new Activity()
            {
                Id = id,
                MachineId = MachineId,
                Machine = Machine,
                WorkshopId = WorkshopId,
                Workshop = Workshop,
                Status = Status,
                Description = Description,
                TypeId = TypeId,
                Type = Type,
                IsLoto = IsLoto,
                IsJog = IsJog,
                IsDurningProduction = IsDurningProduction,
                OccuranceValue = OccuranceValue,
                Occurance = Occurance,
            };
        }
    }
}