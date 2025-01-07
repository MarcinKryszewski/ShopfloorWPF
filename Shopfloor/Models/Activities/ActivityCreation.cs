using Shopfloor.Models.ActivityTypes;
using Shopfloor.Models.Commons.BaseClasses;
using Shopfloor.Models.Commons.Interfaces;
using Shopfloor.Models.Machines;
using Shopfloor.Models.Workshops;

namespace Shopfloor.Models.Activities
{
    internal class ActivityCreation : ModelValidationBase, IModelCreationModel<Activity>
    {
        public string Description { get; set; } = string.Empty;
        public int Id { get; set; }
        public bool IsDurningProduction { get; set; }
        public bool IsJog { get; set; }
        public bool IsLoto { get; set; }
        public bool IsPassed { get; set; } = false;
        public Machine? Machine { get; set; }
        public int MachineId { get; set; }
        public Occurance Occurance { get; set; } = new();
        public int OccuranceValue { get; set; } = 1;
        public ActivityType? Type { get; set; }
        public int TypeId { get; set; }
        public Workshop? Workshop { get; set; }
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
                Description = Description,
                IsPassed = IsPassed,
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