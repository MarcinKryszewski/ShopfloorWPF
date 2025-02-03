using System.Linq;
using Shopfloor.Models.ActivityTypes;
using Shopfloor.Models.Commons.Interfaces;
using Shopfloor.Models.Machines;
using Shopfloor.Models.Workshops;

namespace Shopfloor.Models.Activities
{
    internal class Activity : IModel
    {
        public string Additionals => string.Join(
            " / ",
            new[]
            {
                IsLoto ? "LOTO" : null,
                IsJog ? "JOG" : null,
                IsDurningProduction ? "Podczas produkcji" : null,
            }.Where(s => !string.IsNullOrEmpty(s)));
        public string Description { get; set; } = string.Empty;
        public bool HasInstruction { get; set; } = false;
        required public int Id { get; init; }
        public bool IsDurningProduction { get; set; }
        public bool IsJog { get; set; }
        public bool IsLoto { get; set; }
        public Machine? Machine { get; set; }
        required public int MachineId { get; init; }
        public string Name { get; } = string.Empty;
        public Occurance Occurance { get; set; } = new();
        public string OccuranceText => $"{OccuranceValue} {Occurance.OccuranceUnitText}";
        public OccuranceUnit OccuranceUnit
        {
            get => Occurance.Unit;
            set => Occurance.Unit = value;
        }
        public int OccuranceValue { get; set; } = 1;
        public ActivityStatus Status { get; set; } = ActivityStatus.Unconfirmed;
        public string StatusText => Status.ToString();
        public ActivityType? Type { get; set; }
        required public int TypeId { get; init; }
        public Workshop? Workshop { get; set; }
        required public int WorkshopId { get; init; }
        public Activity Clone()
        {
            return (Activity)MemberwiseClone();
        }
        public void SetValues<T>(IModelCreationModel<T> data)
                    where T : IModel
        {
            if (data is not ActivityCreation)
            {
                return;
            }

            ActivityCreation creation = (ActivityCreation)data;

            Machine = creation.Machine;
            Workshop = creation.Workshop;
            Status = creation.Status;
            Description = creation.Description;
            Type = creation.Type;
            IsLoto = creation.IsLoto;
            IsJog = creation.IsJog;
            IsDurningProduction = creation.IsDurningProduction;
            OccuranceValue = creation.OccuranceValue;
            Occurance = creation.Occurance;
        }
    }
}