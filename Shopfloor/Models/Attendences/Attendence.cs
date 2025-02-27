using Shopfloor.Models.Commons.Interfaces;
using Shopfloor.Models.Persons;

namespace Shopfloor.Models.Attendences
{
    internal class Attendence : IModel
    {
        public int Id { get; init; }
        public string Name { get; set; } = string.Empty;
        public bool IsConfirmedByCoach { get; set; }
        public bool IsConfirmedByTrainee { get; set; }
        public AttendenceCreation CreateModelCreation()
        {
            return new AttendenceCreation()
            {
                Id = Id,
                IsConfirmedByCoach = IsConfirmedByCoach,
                IsConfirmedByTrainee = IsConfirmedByTrainee,
            };
        }
        public void SetValues<T>(IModelCreationModel<T> data)
        where T : IModel
        {
            if (data is not AttendenceCreation)
            {
                return;
            }

            AttendenceCreation creation = (AttendenceCreation)data;

            IsConfirmedByCoach = creation.IsConfirmedByCoach;
            IsConfirmedByTrainee = creation.IsConfirmedByTrainee;
            Name = creation.Name;
        }
    }
}